namespace Otchitta.Library.Matrix.Rdb;

/// <summary>
/// RDB利用読込格子基本情報です。
/// </summary>
public sealed class RdbReadableMatrixData : ReadableMatrixData {
	#region メンバー変数定義
	/// <summary>表題一覧</summary>
	private readonly RdbReadableColumnList columnList;
	/// <summary>要素一覧</summary>
	private readonly RdbReadableRecordList recordList;
	#endregion メンバー変数定義

	#region プロパティー定義
	/// <summary>
	/// 表題一覧を取得します。
	/// </summary>
	/// <returns>表題一覧</returns>
	public ReadableColumnList ColumnList {
		get => this.columnList;
	}
	/// <summary>
	/// 要素一覧を取得します。
	/// </summary>
	/// <returns>表題一覧</returns>
	public ReadableRecordList RecordList {
		get => this.recordList;
	}
	#endregion プロパティー定義

	#region 生成メソッド定義
	/// <summary>
	/// RDB利用読込格子基本情報を生成します。
	/// </summary>
	/// <param name="columnList">表題一覧</param>
	/// <param name="recordList">要素一覧</param>
	private RdbReadableMatrixData(RdbReadableColumnList columnList, RdbReadableRecordList recordList) {
		this.columnList = columnList;
		this.recordList = recordList;
	}
	/// <summary>
	/// RDB利用読込格子基本情報を生成します。
	/// </summary>
	/// <param name="source">読込処理</param>
	/// <returns>RDB利用読込格子基本情報</returns>
	private static async Task<RdbReadableMatrixData> CreateData(System.Data.Common.DbDataReader source) {
		var columnList =       RdbReadableColumnList.CreateData(source);
		var recordList = await RdbReadableRecordList.CreateData(source);
		return new (columnList, recordList);
	}
	#endregion 生成メソッド定義

	/// <summary>
	/// RDB利用読込格子基本情報を生成します。
	/// </summary>
	/// <param name="source">実行処理</param>
	/// <returns>RDB利用読込格子基本情報</returns>
	/// <exception cref="Common.Service.Rdb.RdbServiceException">実行処理に失敗した場合</exception>
	public static async Task<RdbReadableMatrixData> CreateData(System.Data.Common.DbCommand source) {
		try {
			using var reader = await Common.Service.Rdb.RdbServiceExtension.InvokeList(source);
			return await CreateData(reader);
		} catch (Common.Service.Rdb.RdbServiceException) {
			throw;
		} catch (Exception errors) {
			throw new Common.Service.Rdb.RdbServiceException("格子処理の実行に失敗しました。", source, errors);
		}
	}
}
