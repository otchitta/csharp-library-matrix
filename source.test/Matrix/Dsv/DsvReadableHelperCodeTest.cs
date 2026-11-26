namespace Otchitta.Library.Matrix.Dsv;

/// <summary>
/// DSV利用読込格子補助関数検証です。
/// </summary>
[TestFixture]
public class DsvReadableHelperCodeTest {
	#region 内部メソッド定義：ImportTest
	/// <summary>
	/// 取込情報を取得します。
	/// </summary>
	/// <param name="source">読込名称</param>
	/// <returns>取込情報</returns>
	private static string ImportText(string source) {
		return File.ReadAllText(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "Matrix", "Dsv", source)));
	}
	#endregion 内部メソッド定義：ImportTest

	#region 内部メソッド定義：AssertData
	/// <summary>
	/// 要素情報を検証します。
	/// </summary>
	/// <param name="source">要素情報</param>
	/// <param name="expect">想定配列</param>
	private static void AssertData(ReadableRecordData source, params object?[] expect) {
		Assert.That(source, Has.Count.EqualTo(expect.Length));
		for (var index = 0; index < source.Count; index ++) {
			Assert.That(source[index], Is.EqualTo(expect[index]), $"{index + 1:000}の要素が異なります");
		}
	}
	#endregion 内部メソッド定義：AssertData

	#region 検証メソッド定義：TestCase01
	/// <summary>
	/// <see cref="DsvReadableHelperCode.DecodeData(ReadOnlySpan{char}, char, char, string)" />を検証します。
	/// </summary>
	[Test]
	public void TestCase01() {
		var source = ImportText("DsvReadableHelperCode0001.csv");
		var result = DsvReadableHelperCode.DecodeData(source, '"', ',', "\r\n");
		Assert.That(result, Has.Count.EqualTo(3));
		AssertData(result[0], "Column1", "Column2", "Column3", "Column4", "Column5");
		AssertData(result[1], "Value11", "Value12", "Value13", "Value14", "Value15");
		AssertData(result[2], "Value21", "Value22", "Value23", "Value24");
	}
	#endregion 検証メソッド定義：TestCase01
}
