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

using Google.Cloud.Workflows.Executions.V1;

[Collection(nameof(WorkflowFixture))]
public class ExecuteWithArgumentsTests
{
    private readonly ExecuteWorkflowWithArgumentsSample _sample;
    private readonly WorkflowFixture _fixture;

    public ExecuteWithArgumentsTests(WorkflowFixture fixture)
    {
        _fixture = fixture;
        _sample = new ExecuteWorkflowWithArgumentsSample();
    }

    [Fact]
    public async Task ExecuteWithArguments()
    {
        Task<Execution> executionTask = _sample.ExecuteWorkflowWithArguments(_fixture.ProjectId, _fixture.LocationId, _fixture.WorkflowID);
        var completedTask = await Task.WhenAny(executionTask, Task.Delay(TimeSpan.FromMinutes(10)));
        if (completedTask != executionTask)
        {
            throw new TimeoutException("The operation has timed out.");
        }

        Execution execution = await executionTask;

        // Validate if execution was successful.
        Assert.Equal(Execution.Types.State.Succeeded, execution.State);

        // Validate if execution result contains expected value.
        Assert.Contains("Cloud", execution.Result);
    }
}
