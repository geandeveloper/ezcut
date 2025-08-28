namespace Common.Observability
{
    public readonly struct MetricDefinition(string name, string description = "")
    {
        public string Name { get; } = name;
        public string Description { get; } = description;

        public override string ToString() => Name;
    }
}
