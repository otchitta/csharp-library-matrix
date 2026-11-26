namespace Otchitta.Library.Matrix.Dsv;

/// <summary>
/// DSV利用読込格子要素一覧です。
/// </summary>
internal sealed class DsvReadableRecordList : ReadableRecordList {
	#region メンバー変数定義
	/// <summary>要素配列</summary>
	private readonly DsvReadableRecordData[] source;
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
	/// DSV利用読込格子要素一覧を生成します。
	/// </summary>
	/// <param name="source">要素配列</param>
	internal DsvReadableRecordList(DsvReadableRecordData[] source) {
		this.source = source;
	}
	#endregion 生成メソッド定義
}
