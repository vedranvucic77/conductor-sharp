﻿namespace ConductorSharp.Engine.Tests.Samples.Workflows;


internal class CSharpLambdaInput : WorkflowInput<CSharpLambdaOutput>
{
    public string Input { get; set; }
}

internal class CSharpLambdaOutput : WorkflowOutput
{

}

internal class CSharpLambda : Workflow<CSharpLambdaInput, CSharpLambdaOutput>
{

    #region Input and output classes
    internal class IdentityInput : IRequest<IdentityInput>
    {
        public string Input { get; set; }
    }

    internal class StringInput : IRequest<StringOutput>
    {
        public string Input { get; set; }
    }

    internal class StringOutput
    {
        public string Output { get; set; }
    }
    #endregion

    public LambdaTaskModel<IdentityInput, IdentityInput> Identity { get; set; }
    public LambdaTaskModel<StringInput, StringOutput> ToLowerInvariant { get; set; }
    public LambdaTaskModel<StringInput, StringOutput> ToUpperInvariant { get; set; }
    public LambdaTaskModel<StringInput, StringOutput> TrimStart { get; set; }
    public LambdaTaskModel<StringInput, StringOutput> TrimStartSingleChar { get; set; }
    public LambdaTaskModel<StringInput, StringOutput> TrimStartMultipleChars { get; set; }
    public LambdaTaskModel<StringInput, StringOutput> TrimEnd { get; set; }
    public LambdaTaskModel<StringInput, StringOutput> TrimEndSingleChar { get; set; }
    public LambdaTaskModel<StringInput, StringOutput> TrimEndMultipleChars { get; set; }
    public LambdaTaskModel<StringInput, StringOutput> Trim { get; set; }
    public LambdaTaskModel<StringInput, StringOutput> TrimSingleChar { get; set; }
    public LambdaTaskModel<StringInput, StringOutput> TrimMultipleChars { get; set; }

    public override WorkflowDefinition GetDefinition()
    {
        var builder = new WorkflowDefinitionBuilder<CSharpLambda>();

        builder.AddTask(wf => wf.Identity, wf => new()
        {
            Input = wf.WorkflowInput.Input
        }, input => input);

        builder.AddTask(wf => wf.ToLowerInvariant, wf => new()
        {
            Input = wf.WorkflowInput.Input
        }, input => new StringOutput { Output = input.Input.ToLowerInvariant() });

        builder.AddTask(wf => wf.ToUpperInvariant, wf => new()
        {
            Input = wf.WorkflowInput.Input
        }, input => new StringOutput { Output = input.Input.ToUpperInvariant() });

        builder.AddTask(wf => wf.TrimStart, wf => new()
        {
            Input = wf.WorkflowInput.Input
        }, input => new StringOutput { Output = input.Input.TrimStart() });

        builder.AddTask(wf => wf.TrimStartSingleChar, wf => new()
        {
            Input = wf.WorkflowInput.Input
        }, input => new StringOutput { Output = input.Input.TrimStart('/') });

        builder.AddTask(wf => wf.TrimStartMultipleChars, wf => new()
        {
            Input = wf.WorkflowInput.Input
        }, input => new StringOutput { Output = input.Input.TrimStart('/', '.') });

        builder.AddTask(wf => wf.TrimEnd, wf => new()
        {
            Input = wf.WorkflowInput.Input
        }, input => new StringOutput { Output = input.Input.TrimEnd() });

        builder.AddTask(wf => wf.TrimEndSingleChar, wf => new()
        {
            Input = wf.WorkflowInput.Input
        }, input => new StringOutput { Output = input.Input.TrimEnd('/') });

        builder.AddTask(wf => wf.TrimEndMultipleChars, wf => new()
        {
            Input = wf.WorkflowInput.Input
        }, input => new StringOutput { Output = input.Input.TrimEnd('/', '.') });

        builder.AddTask(wf => wf.Trim, wf => new()
        {
            Input = wf.WorkflowInput.Input
        }, input => new StringOutput { Output = input.Input.Trim() });

        builder.AddTask(wf => wf.TrimSingleChar, wf => new()
        {
            Input = wf.WorkflowInput.Input
        }, input => new StringOutput { Output = input.Input.Trim('/') });

        builder.AddTask(wf => wf.TrimMultipleChars, wf => new()
        {
            Input = wf.WorkflowInput.Input
        }, input => new StringOutput { Output = input.Input.Trim('/', '.') });

        return builder.Build(opts => opts.Version = 1);
    }
}
