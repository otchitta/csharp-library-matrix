namespace Otchitta.Library.Matrix.Dsv;

/// <summary>
/// DSV利用読込格子補助関数です。
/// </summary>
internal static class DsvReadableHelperCode {
	#region 内部メソッド定義：UpdateData/AcceptData
	/// <summary>
	/// 分割情報を更新します。
	/// </summary>
	/// <param name="source">分割情報</param>
	/// <param name="choose">読込情報</param>
	private static void UpdateData(char[] source, char choose) {
		for (var index = 1; index < source.Length; index ++) {
			source[index - 1] = source[index];
		}
		source[^1] = choose;
	}
	/// <summary>
	/// 分割情報を判定します。
	/// </summary>
	/// <param name="value1"></param>
	/// <param name="value2"></param>
	/// <returns></returns>
	private static bool AcceptData(char[] value1, string value2) {
		for (var index = 0; index < value1.Length; index ++) {
			if (value1[index] != value2[index]) return false;
		}
		return true;
	}
	#endregion 内部メソッド定義：UpdateData/AcceptData

	#region 内部メソッド定義：DecodeText/DecodeItem/DecodeLine
	/// <summary>
	/// 文字情報へ復号します。
	/// </summary>
	/// <param name="source">読込情報</param>
	/// <param name="escape">回避文字</param>
	/// <returns>文字情報</returns>
	/// <exception cref="Common.Service.Dsv.DsvServiceException"></exception>
	private static string DecodeText(ReadOnlySpan<char> source, char escape) {
		var result = new StringBuilder();
		var before = -1;
		for (var index = 0; index < source.Length; index ++) {
			var choose = source[index];
			if (before != escape) {
				result.Append(choose);
				before = choose;
			} else if (choose == escape) {
				before = -1;
			} else {
				throw new Common.Service.Dsv.DsvServiceException("Must be non duplicated escape.", ("source", source.ToString()), ("escape", escape));
			}
		}
		if (before == escape) {
			throw new Common.Service.Dsv.DsvServiceException("Must be non duplicated escape.", ("source", source.ToString()), ("escape", escape));
		} else {
			return result.ToString();
		}
	}
	/// <summary>
	/// 項目情報へ復号します。
	/// </summary>
	/// <param name="source">読込情報</param>
	/// <param name="escape">回避文字</param>
	/// <returns>項目情報</returns>
	private static string? DecodeItem(ReadOnlySpan<char> source, char escape) {
		return source.Length switch {
			0 => null,
			1 => DecodeText(source, escape),
			_ => DecodeText((source[0] == escape && source[^1] == escape) ? source[1..^1] : source, escape),
		};
	}
	/// <summary>
	/// 要素情報へ復号します。
	/// </summary>
	/// <param name="source">読込情報</param>
	/// <param name="escape">回避文字</param>
	/// <param name="marker">分割文字</param>
	/// <returns>要素情報</returns>
	private static DsvReadableRecordData DecodeLine(ReadOnlySpan<char> source, char escape, char marker) {
		var offset = 0;
		var ignore = false;
		var result = new List<string?>();
		for (var index = 0; index < source.Length; index ++) {
			var choose = source[index];
			if (choose == escape) {
				// 回避文字である場合：状態反転
				ignore = !ignore;
			} else if (ignore) {
				// 除外状態である場合：処理なし
			} else if (choose == marker) {
				// 分割文字である場合：項目構築
				result.Add(DecodeItem(source[offset..index], escape));
				offset = index + 1;
			} else {
				// 上記以外である場合：処理なし
			}
		}
		if (offset < source.Length) {
			result.Add(DecodeItem(source[offset..], escape));
		}
		return new ([.. result]);
	}
	#endregion 内部メソッド定義：DecodeText/DecodeItem/DecodeLine

	#region 公開メソッド定義：DecodeData
	/// <summary>
	/// 要素一覧へ復号します。
	/// </summary>
	/// <param name="source">読込情報</param>
	/// <param name="escape">回避文字</param>
	/// <param name="marker">分割文字</param>
	/// <param name="anchor">改行文字</param>
	/// <returns>要素一覧</returns>
	public static DsvReadableRecordList DecodeData(ReadOnlySpan<char> source, char escape, char marker, string anchor) {
		var ignore = false;
		var values = new char[anchor.Length];
		var offset = 0;
		var result = new List<DsvReadableRecordData>();
		for (var index = 0; index < source.Length; index ++) {
			var choose = source[index];
			UpdateData(values, choose);
			if (choose == escape) {
				// 回避文字である場合：状態反転
				ignore = !ignore;
			} else if (ignore) {
				// 除外状態である場合：処理なし
			} else if (AcceptData(values, anchor)) {
				// 分割文字である場合：要素構築
				result.Add(DecodeLine(source.Slice(offset, index - offset - anchor.Length + 1), escape, marker));
				offset = index + 1;
			} else {
				// 上記以外である場合：処理なし
			}
		}
		if (offset < source.Length) {
			result.Add(DecodeLine(source[offset..], escape, marker));
		}
		return new ([.. result]);
	}
	#endregion 公開メソッド定義：DecodeData
}
