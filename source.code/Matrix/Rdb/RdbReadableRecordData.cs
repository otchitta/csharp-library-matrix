namespace Otchitta.Library.Matrix.Rdb;

/// <summary>
/// RDB利用読込格子要素情報です。
/// </summary>
internal sealed class RdbReadableRecordData : ReadableRecordData {
	#region メンバー変数定義
	/// <summary>項目配列</summary>
	private readonly object?[] source;
	#endregion メンバー変数定義

	#region プロパティー定義
	/// <summary>
	/// 項目個数を取得します。
	/// </summary>
	/// <returns>項目個数</returns>
	public int Count {
		get => this.source.Length;
	}
	/// <summary>
	/// 項目情報を取得します。
	/// </summary>
	/// <param name="index">項目番号</param>
	/// <returns>項目情報</returns>
	public object? this[int index] {
		get => this.source[index];
	}
	#endregion プロパティー定義

	#region 生成メソッド定義
	/// <summary>
	/// RDB利用読込格子要素情報を生成します。
	/// </summary>
	/// <param name="source">項目配列</param>
	private RdbReadableRecordData(object?[] source) {
		this.source = source;
	}
	/// <summary>
	/// RDB利用読込格子要素情報を生成します。
	/// </summary>
	/// <param name="source">読込情報</param>
	/// <returns>RDB利用読込格子要素情報</returns>
	internal static RdbReadableRecordData CreateData(System.Data.IDataRecord source) {
		var result = new object?[source.FieldCount];
		for (var index = 0; index < result.Length; index ++) {
			result[index] = source.IsDBNull(index)? null: source[index];
		}
		return new (result);
	}
	#endregion 生成メソッド定義
}
