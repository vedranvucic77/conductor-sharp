﻿using ConductorSharp.Client.Model.Common;
using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util.Builders;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ConductorSharp.Engine.Builders
{
    public static class TerminateTaskExtensions
    {
        public static ITaskOptionsBuilder AddTask<TWorkflow>(
            this ITaskSequenceBuilder<TWorkflow> builder,
            Expression<Func<TWorkflow, TerminateTaskModel>> reference,
            Expression<Func<TWorkflow, TerminateTaskInput>> input
        ) where TWorkflow : ITypedWorkflow
        {
            var taskBuilder = new TerminateTaskBuilder(reference.Body, input.Body, builder.BuildConfiguration);
            builder.AddTaskBuilderToSequence(taskBuilder);
            return taskBuilder;
        }
    }

    internal class TerminateTaskBuilder : BaseTaskBuilder<TerminateTaskInput, NoOutput>
    {
        public TerminateTaskBuilder(Expression taskExpression, Expression inputExpression, BuildConfiguration buildConfiguration)
            : base(taskExpression, inputExpression, buildConfiguration) { }

        public override WorkflowDefinition.Task[] Build() =>
            new[]
            {
                new WorkflowDefinition.Task
                {
                    Name = $"TERMINATE_{_taskRefferenceName}",
                    TaskReferenceName = _taskRefferenceName,
                    Type = "TERMINATE",
                    InputParameters = _inputParameters,
                    Description = new JObject { new JProperty("description", _description) }.ToString(Newtonsoft.Json.Formatting.None),
                }
            };
    }
}
