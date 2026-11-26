namespace Otchitta.Library.Matrix;

/// <summary>
/// 読込格子表題一覧です。
/// </summary>
public interface ReadableColumnList : IReadOnlyList<string> {
	/// <summary>
	/// 反復処理を取得します。
	/// </summary>
	/// <returns>反復処理</returns>
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
		for (var index = 0; index < Count; index ++) {
			yield return this[index];
		}
	}
	/// <summary>
	/// 反復処理を取得します。
	/// </summary>
	/// <returns>反復処理</returns>
	IEnumerator<string> IEnumerable<string>.GetEnumerator() {
		for (var index = 0; index < Count; index ++) {
			yield return this[index];
		}
	}
}
