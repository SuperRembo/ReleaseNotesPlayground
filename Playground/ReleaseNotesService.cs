namespace Playground;

class ReleaseNotesService
{
	readonly List<ReleaseNote> releaseNotes;

	readonly List<ReadReceipt> readReceipts = new();

	public ReleaseNotesService(List<ReleaseNote> releaseNotes)
	{
		this.releaseNotes = releaseNotes;
	}

	public Task<bool> HasUnreadItemsAsync(string userCode)
	{
		var receipt = FindReadReceipt(userCode);

		var hasUnreadItems = receipt != null
			? HasReleaseNotesAfter(receipt.ReleaseDate)
			: HasReleaseNotes();

		return Task.FromResult(hasUnreadItems);
	}

	public Task MarkAsReadAsync(string userCode)
	{
		var lastReleaseDate = releaseNotes.Max(i => i.ReleaseDate);

		var receipt = FindReadReceipt(userCode);
		if (receipt != null)
			receipt.ReleaseDate = lastReleaseDate;
		else
			readReceipts.Add(new ReadReceipt(userCode, lastReleaseDate));

		return Task.CompletedTask;
	}

	bool HasReleaseNotes() => releaseNotes.Any();

	bool HasReleaseNotesAfter(DateTime releaseDate) => releaseNotes.Any(i => i.ReleaseDate > releaseDate);

	ReadReceipt? FindReadReceipt(string userCode) => readReceipts.FirstOrDefault(i => i.UserCode == userCode);
}

record ReleaseNote(string Version, DateTime ReleaseDate);

class ReadReceipt
{
	public ReadReceipt(string userCode, DateTime releaseDate)
	{
		UserCode = userCode;
		ReleaseDate = releaseDate;
	}

	public string UserCode { get; init; }
	public DateTime ReleaseDate { get; set; }

}