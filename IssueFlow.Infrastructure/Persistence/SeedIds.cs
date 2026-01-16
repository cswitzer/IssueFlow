namespace IssueFlow.Infrastructure.Persistence;

internal static class SeedIds
{
    internal static class IssuePriorities
    {
        internal static readonly Guid Low =
            Guid.Parse("11111111-1111-1111-1111-111111111111");

        internal static readonly Guid Medium =
            Guid.Parse("22222222-2222-2222-2222-222222222222");

        internal static readonly Guid High =
            Guid.Parse("33333333-3333-3333-3333-333333333333");
    }

    internal static class IssueStatuses
    {
        internal static readonly Guid Todo =
            Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

        internal static readonly Guid InProgress =
            Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

        internal static readonly Guid Blocked =
            Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

        internal static readonly Guid Done =
            Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

        internal static readonly Guid Resolved =
            Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
    }

    internal static class IssueTypes
    {
        internal static readonly Guid Story =
            Guid.Parse("44444444-4444-4444-4444-444444444444");

        internal static readonly Guid Bug =
            Guid.Parse("55555555-5555-5555-5555-555555555555");

        internal static readonly Guid Task =
            Guid.Parse("66666666-6666-6666-6666-666666666666");

        internal static readonly Guid Epic =
            Guid.Parse("77777777-7777-7777-7777-777777777777");
    }
}
