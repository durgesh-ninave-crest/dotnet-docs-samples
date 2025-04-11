/*
 * Copyright 2025 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Google.Cloud.Kms.V1;
using Google.Cloud.ParameterManager.V1;
using Google.Protobuf;
using System.Text;

[Collection(nameof(ParameterManagerFixture))]
public class UpdateParameterKmsKeyTests
{
    private readonly ParameterManagerFixture _fixture;
    private readonly UpdateParameterKmsKeySample _sample;

    public UpdateParameterKmsKeyTests(ParameterManagerFixture fixture)
    {
        _fixture = fixture;
        _sample = new UpdateParameterKmsKeySample();
    }

    [Fact]
    public void UpdateParameterKmsKey()
    {
        string KeyId = _fixture.RandomId();
        string KeyId1 = _fixture.RandomId();
        CryptoKey cryptoKey = _fixture.CreateHsmKey(_fixture.ProjectId, KeyId, "csharp-test-key-ring");
        CryptoKey cryptoKey1 = _fixture.CreateHsmKey(_fixture.ProjectId, KeyId1, "csharp-test-key-ring");
        CryptoKeyVersionName cryptoKeyVersionName = new CryptoKeyVersionName(_fixture.ProjectId, ParameterManagerFixture.LocationId, "csharp-test-key-ring", KeyId, "1");
        CryptoKeyVersionName cryptoKeyVersionName1 = new CryptoKeyVersionName(_fixture.ProjectId, ParameterManagerFixture.LocationId, "csharp-test-key-ring", KeyId1, "1");

        string parameterId = _fixture.RandomId();
        _fixture.CreateParameterWithKmsKey(parameterId, ParameterFormat.Unformatted, cryptoKey.Name);

        Parameter result = _sample.UpdateParameterKmsKey(
          projectId: _fixture.ProjectId, parameterId: parameterId, kmsKey: cryptoKey1.Name);

        Assert.NotNull(result);
        Assert.Equal(result.ParameterName.ParameterId, parameterId);
        Assert.Equal(result.KmsKey, cryptoKey1.Name);

        _fixture.ParametersToDelete.Add(result.ParameterName);
        _fixture.CryptoKeyVersionsToDelete.Add(cryptoKeyVersionName);
        _fixture.CryptoKeyVersionsToDelete.Add(cryptoKeyVersionName1);
    }
}
