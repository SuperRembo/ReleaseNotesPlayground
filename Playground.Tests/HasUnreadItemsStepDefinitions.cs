using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Playground.Tests
{
	[Binding]
	public class HasUnreadItemsStepDefinitions
	{
		readonly List<ReleaseNote> releaseNotes = new();
		readonly ReleaseNotesService sut;
		string userCode;

		public HasUnreadItemsStepDefinitions()
		{
			sut = new ReleaseNotesService(releaseNotes);
		}

		[Given(@"user (.*)")]
		public void GivenUserInOrganisation(string user)
		{
			userCode = user;
		}

		[Given(@"release notes:")]
		public void GivenReleaseNotes(Table table)
		{
			foreach (var releaseNote in table.CreateSet<ReleaseNote>())
				releaseNotes.Add(releaseNote);
		}

		[Given(@"the current release notes are marked as read")]
		public async Task GivenTheCurrentReleaseNotesAreMarkedAsRead()
		{
			await sut.MarkAsReadAsync(userCode);
		}

		[When(@"marking release notes as read")]
		public async Task WhenMarkingReleaseNotesAsRead()
		{
			await sut.MarkAsReadAsync(userCode);
		}

		[Then(@"has unread items should be (.*)")]
		public async Task ThenHasUnreadItemsShouldBeFalse(bool status)
		{
			var hasUnreadItems = await sut.HasUnreadItemsAsync(userCode);
			hasUnreadItems.Should().Be(status);
		}
	}
}
