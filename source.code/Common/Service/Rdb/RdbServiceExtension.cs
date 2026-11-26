namespace Otchitta.Library.Common.Service.Rdb;

/// <summary>
/// RDB利用拡張関数です。
/// </summary>
public static class RdbServiceExtension {
	/// <summary>
	/// 実行処理を実行します。
	/// </summary>
	/// <param name="source">実行処理</param>
	/// <param name="cancel">取消処理</param>
	/// <returns>読込処理</returns>
	/// <exception cref="RdbServiceException">抽出処理に失敗した場合</exception>
	internal static async Task<System.Data.Common.DbDataReader> InvokeList(System.Data.Common.DbCommand source, CancellationToken cancel = default) {
		try {
			return await source.ExecuteReaderAsync(cancel);
		} catch (Exception errors) {
			throw new RdbServiceException("読込処理に失敗しました。", source, errors);
		}
	}
	/// <summary>
	/// 実行処理を実行します。
	/// </summary>
	/// <typeparam name="TResult"></typeparam>
	/// <param name="source">実行処理</param>
	/// <param name="change">変換処理</param>
	/// <param name="cancel">取消処理</param>
	/// <exception cref="RdbServiceException">抽出処理に失敗した場合</exception>
	public static async Task<TResult> InvokeData<TResult>(this System.Data.Common.DbCommand source, Func<System.Data.Common.DbDataReader, TResult> change, CancellationToken cancel = default) {
		return change(await InvokeList(source, cancel)); // TODO 変換処理に失敗した場合の例外発行を行う
	}
}
