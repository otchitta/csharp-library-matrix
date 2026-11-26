namespace Otchitta.Library.Matrix.Rdb;

/// <summary>
/// RDB利用読込格子要素一覧です。
/// </summary>
internal sealed class RdbReadableRecordList : ReadableRecordList {
	#region メンバー変数定義
	/// <summary>要素配列</summary>
	private readonly RdbReadableRecordData[] source;
	#endregion メンバー変数定義

	#region プロパティー定義
	/// <summary>
	/// 要素個数を取得します。
	/// </summary>
	/// <returns>要素個数</returns>
	public int Count {
		get => this.source.Length;
	}
	/// <summary>
	/// 要素情報を取得します。
	/// </summary>
	/// <param name="index">要素番号</param>
	/// <returns>要素情報</returns>
	public ReadableRecordData this[int index] {
		get => this.source[index];
	}
	#endregion プロパティー定義

	#region 生成メソッド定義
	/// <summary>
	/// RDB利用読込格子要素一覧を生成します。
	/// </summary>
	/// <param name="source">要素配列</param>
	private RdbReadableRecordList(RdbReadableRecordData[] source) {
		this.source = source;
	}
	/// <summary>
	/// RDB利用読込格子要素一覧を生成します。
	/// </summary>
	/// <param name="source">読込情報</param>
	/// <returns>RDB利用読込格子要素一覧</returns>
	internal static async Task<RdbReadableRecordList> CreateData(System.Data.Common.DbDataReader source) {
		var result = new List<RdbReadableRecordData>();
		while (await source.ReadAsync()) {
			result.Add(RdbReadableRecordData.CreateData(source));
		}
		return new ([.. result]);
	}
	#endregion 生成メソッド定義
}
