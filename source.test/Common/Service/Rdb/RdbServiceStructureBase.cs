namespace Otchitta.Library.Common.Service.Rdb;

/// <summary>
/// RDB利用基底処理です。
/// </summary>
public abstract class RdbServiceStructureBase {
	#region 内部メソッド定義
	/// <summary>
	/// 実行構文を実行します。
	/// </summary>
	/// <param name="commandData">実行処理</param>
	/// <param name="commandText">実行構文</param>
	private static async Task InvokeCode(DbCommand commandData, string commandText) {
		commandData.CommandText = commandText;
		commandData.Parameters.Clear();
		await commandData.ExecuteNonQueryAsync();
	}
	/// <summary>
	/// 実行一覧を実行します。
	/// </summary>
	/// <param name="commandData">実行処理</param>
	/// <param name="commandList">実行一覧</param>
	private static async Task InvokeCode(DbCommand commandData, params string[] commandList) {
		commandData.Parameters.Clear();
		for (var index = 0; index < commandList.Length; index ++) {
			commandData.CommandText = commandList[index];
			await commandData.ExecuteNonQueryAsync();
		}
	}
	#endregion 内部メソッド定義

	#region 公開メソッド定義
	/// <summary>
	/// 接続処理を生成します。
	/// </summary>
	/// <returns>接続処理</returns>
	protected static async Task<DbConnection> CreateData() {
		var result = new System.Data.SQLite.SQLiteConnection("Data Source=:Memory:;");
		await result.OpenAsync();
		return result;
	}
	/// <summary>
	/// 接続処理を生成します。
	/// </summary>
	/// <param name="source">接続処理</param>
	/// <returns>接続処理</returns>
	protected static async Task<DbCommand> CreateData(DbConnection source) {
		var result = source.CreateCommand();
		await InvokeCode(result, @"CREATE TABLE d_success (
    f_code INTEGER      NOT NULL
  , f_name VARCHAR(100) NOT NULL
  , CONSTRAINT pk_success PRIMARY KEY(f_code)
);");
		await InvokeCode(result, "INSERT INTO d_success(f_code, f_name) VALUES(1, 'NAME-1')");
		await InvokeCode(result, "INSERT INTO d_success(f_code, f_name) VALUES(2, 'NAME-2')");
		await InvokeCode(result, "INSERT INTO d_success(f_code, f_name) VALUES(3, 'NAME-3')");
		return result;
	}
	#endregion 公開メソッド定義

	// /// <summary>
	// /// 実行処理を生成します。
	// /// </summary>
	// /// <param name="source">接続処理</param>
	// /// <returns>実行処理</returns>
	// protected static async Task<DbCommand> CreateData(DbConnection source) {
	// 	var result = source.CreateCommand();
	// 	return result;
	// }

	// /// <summary>
	// /// 例外内容を取得します。
	// /// </summary>
	// /// <param name="source"></param>
	// /// <returns></returns>
	// protected static string ChooseTest(string source) {
	// 	return File.ReadAllText(Path.Combine("Common", "Service", "Rdb", source), System.Text.Encoding.UTF8);
	// }
}
