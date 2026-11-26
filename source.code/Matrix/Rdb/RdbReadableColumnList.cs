namespace Otchitta.Library.Matrix.Rdb;

/// <summary>
/// RDB利用読込格子表題一覧です。
/// </summary>
internal sealed class RdbReadableColumnList : ReadableColumnList {
	#region メンバー変数定義
	/// <summary>表題配列</summary>
	private readonly string[] source;
	#endregion メンバー変数定義

	#region プロパティー定義
	/// <summary>
	/// 表題個数を取得します。
	/// </summary>
	/// <returns>表題個数</returns>
	public int Count {
		get => this.source.Length;
	}
	/// <summary>
	/// 表題名称を取得します。
	/// </summary>
	/// <param name="index">表題番号</param>
	/// <returns>表題名称</returns>
	public string this[int index] {
		get => this.source[index];
	}
	#endregion プロパティー定義

	#region 生成メソッド定義
	/// <summary>
	/// RDB利用読込格子表題一覧を生成します。
	/// </summary>
	/// <param name="source">表題配列</param>
	private RdbReadableColumnList(string[] source) {
		this.source = source;
	}
	/// <summary>
	/// RDB利用読込格子表題一覧を生成します。
	/// </summary>
	/// <param name="source">読込情報</param>
	/// <returns>RDB利用読込格子表題一覧</returns>
	internal static RdbReadableColumnList CreateData(System.Data.IDataRecord source) {
		var result = new string[source.FieldCount];
		for (var index = 0; index < result.Length; index ++) {
			result[index] = source.GetName(index);
		}
		return new (result);
	}
	#endregion 生成メソッド定義
}
