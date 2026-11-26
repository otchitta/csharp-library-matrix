namespace Otchitta.Library.Common.Service.Rdb;

/// <summary>
/// RDB利用例外情報検証です。
/// </summary>
public class RdbServiceExceptionTest : RdbServiceStructureBase {
	#region 内部メソッド定義
	/// <summary>
	/// 要素一覧を生成します。
	/// </summary>
	/// <param name="source">読込処理</param>
	/// <returns>要素一覧</returns>
	private static async Task<List<(int, string)>> CreateList(DbDataReader source) {
		var result = new List<(int, string)>();
		while (await source.ReadAsync()) {
			var code = source.GetInt32(0);
			var name = source.GetString(1);
			result.Add((code, name));
		}
		return result;
	}
	#endregion 内部メソッド定義

	[Test]
	public async Task TestCase01() {
		using var connect = await CreateData();
		using var command = await CreateData(connect);
		command.CommandText = "SELECT * FROM d_failure_data";
		var result = Assert.ThrowsAsync<RdbServiceException>(() => RdbServiceExtension.InvokeData(command, CreateList));
		//Assert.That(result.Message, Is.EqualTo(ChooseTest("RdbServiceStructureTest.01")));
		Assert.That(result.Message, Is.EqualTo(@"読込処理に失敗しました。
Text:SELECT * FROM d_failure_data"));
		Assert.That(result.MessageText, Is.EqualTo("読込処理に失敗しました。"));
		Assert.That(result.CommandText, Is.EqualTo("SELECT * FROM d_failure_data"));
		Assert.That(result.ElementList, Is.Empty);
	}
}
