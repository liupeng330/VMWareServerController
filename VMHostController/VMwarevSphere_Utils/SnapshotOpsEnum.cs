namespace VMwarevSphere_Utils
{
    internal enum SnapshotOpsEnum
    {
        [StringValue("create")]
        Create = 1,

        [StringValue("list")]
        List,

        [StringValue("revert")]
        Revert,

        [StringValue("removeall")]
        RemoveAll,

        [StringValue("remove")]
        Remove,
    }
}
