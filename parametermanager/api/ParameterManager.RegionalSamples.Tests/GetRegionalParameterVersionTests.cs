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

using Google.Cloud.ParameterManager.V1;

[Collection(nameof(ParameterManagerRegionalFixture))]
public class GetRegionalParameterVersionTests
{
    private readonly ParameterManagerRegionalFixture _fixture;
    private readonly GetRegionalParameterVersionSample _sample;

    public GetRegionalParameterVersionTests(ParameterManagerRegionalFixture fixture)
    {
        _fixture = fixture;
        _sample = new GetRegionalParameterVersionSample();
    }

    [Fact]
    public void GetRegionalParameterVersion()
    {
        string parameterId = _fixture.RandomId();
        string versionId = _fixture.RandomId();
        Parameter parameter = _fixture.CreateParameter(parameterId, ParameterFormat.Unformatted);
        string payload = "test123";
        ParameterVersion parameterVersion = _fixture.CreateParameterVersion(parameterId, versionId, payload);
        ParameterVersion result = _sample.GetRegionalParameterVersion(projectId: _fixture.ProjectId, locationId: ParameterManagerRegionalFixture.LocationId, parameterId: parameterId, versionId: versionId);

        Assert.NotNull(result);
        Assert.Equal(result.ParameterVersionName.ParameterVersionId, versionId);

        _fixture.ParametersToDelete.Add(parameter.ParameterName);
        _fixture.ParameterVersionsToDelete.Add(parameterVersion.ParameterVersionName);
    }
}
