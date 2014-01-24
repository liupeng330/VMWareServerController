namespace VMwarevSphere_Utils
{
    public enum VMStatus
    {
        [StringValue("on")]
        ON = 1,

        [StringValue("off")]
        OFF,

        [StringValue("suspend")]
        SUSPEND,

        [StringValue("reset")]
        RESET,

        [StringValue("rebootGuest")]
        REBOOTGUEST,

        [StringValue("shutdownGuest")]
        SHUTDOWNGUEST,

        [StringValue("standbyGuest")]
        STANDBYGUEST
    }
}
