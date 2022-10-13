﻿using ConductorSharp.Client.Model.Common;
using ConductorSharp.Engine.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ConductorSharp.Engine.Builders
{
    public class DynamicTaskBuilder<I, O> : BaseTaskBuilder<DynamicTaskInput<I, O>, O>
    {
        private const string TaskType = "DYNAMIC";
        private const string DynamicTasknameParam = "task_to_execute";

        public DynamicTaskBuilder(Expression taskExpression, Expression inputExpression) : base(taskExpression, inputExpression) { }

        private class DynamicTaskParameters
        {
            [JsonProperty("task_input")]
            public JObject TaskInput { get; set; }

            [JsonProperty("workflow_version")]
            public string WorkflowVersion { get; set; }

            [JsonProperty(DynamicTasknameParam)]
            public string TaskToExecute { get; set; }
        }

        public override WorkflowDefinition.Task[] Build()
        {
            var parameters = _inputParameters.ToObject<DynamicTaskParameters>();

            parameters.TaskInput.Add(new JProperty(DynamicTasknameParam, parameters.TaskToExecute));

            return new WorkflowDefinition.Task[]
            {
                new WorkflowDefinition.Task
                {
                    Name = _taskRefferenceName,
                    TaskReferenceName = _taskRefferenceName,
                    Type = TaskType,
                    InputParameters = parameters.TaskInput,
                    DynamicTaskNameParam = DynamicTasknameParam,
                    SubWorkflowParam = new WorkflowDefinition.SubWorkflowParam
                    {
                        Name = parameters.TaskToExecute,
                        VersionAsString = parameters.WorkflowVersion
                    },
                    Description = new JObject { new JProperty("description", "A dynamic task") }.ToString(Newtonsoft.Json.Formatting.None)
                }
            };
        }
    }
}