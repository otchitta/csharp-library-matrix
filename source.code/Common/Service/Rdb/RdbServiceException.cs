namespace Otchitta.Library.Common.Service.Rdb;

/// <summary>
/// RDB利用例外情報です。
/// </summary>
public class RdbServiceException : Exception {
	#region メンバー変数定義
	/// <summary>例外理由</summary>
	private readonly string messageText;
	/// <summary>実行構文</summary>
	private readonly string commandText;
	/// <summary>引数一覧</summary>
	private readonly ReadOnlyCollection<(string, object?)> elementList;
	#endregion メンバー変数定義

	#region プロパティー定義
	/// <summary>
	/// 例外理由を取得します。
	/// </summary>
	/// <value>例外理由</value>
	public string MessageText {
		get => this.messageText;
	}
	/// <summary>
	/// 実行構文を取得します。
	/// </summary>
	/// <value>実行構文</value>
	public string CommandText {
		get => this.commandText;
	}
	/// <summary>
	/// 引数一覧を取得します。
	/// </summary>
	/// <value>引数一覧</value>
	public ReadOnlyCollection<(string, object?)> ElementList {
		get => this.elementList;
	}
	#endregion プロパティー定義

	#region 生成メソッド定義
	/// <summary>
	/// RDB利用例外情報を生成します。
	/// </summary>
	/// <param name="messageText">例外理由</param>
	/// <param name="commandData">実行処理</param>
	/// <param name="executeData">例外原因</param>
	public RdbServiceException(string messageText, System.Data.IDbCommand commandData, Exception executeData) : base(ToMessage(messageText, commandData), executeData) {
		this.messageText = messageText;
		this.commandText = ToCommand(commandData);
		this.elementList = ToElement(commandData);
	}
	#endregion 生成メソッド定義

	#region 内部メソッド定義
	/// <summary>
	/// 引数配列へ変換します。
	/// </summary>
	/// <param name="source">実行処理</param>
	/// <returns>引数配列</returns>
	private static ReadOnlyCollection<(string, object?)> ToElement(System.Data.IDbCommand source) {
		var result = new List<(string, object?)>();
		try {
			foreach (var choose in source.Parameters) {
				if (choose is System.Data.IDbDataParameter select) {
					result.Add((select.ParameterName, select.Value));
				} else {
					result.Add(("Unknown", choose?.ToString()));
				}
			}
		} catch (Exception errors) {
			result.Clear();
			result.Add(("!ERROR!", errors));
		}
		return new (result);
	}
	/// <summary>
	/// 表現文言へ変換します。
	/// </summary>
	/// <param name="reason">例外理由</param>
	/// <param name="source">実行処理</param>
	/// <returns>表現文言</returns>
	private static string ToMessage(string reason, System.Data.IDbCommand source) {
		var result = new StringBuilder();
		var offset = 1;
		result.Append(reason);
		result.AppendLine();
		result.Append("Text:");
		result.Append(ToCommand(source));
		foreach (var choose in ToElement(source)) {
			result.AppendLine();
			result.AppendFormat(offset.ToString("0000"));
			result.Append(':');
			result.Append(choose);
		}
		return result.ToString();
	}

	/// <summary>
	/// 実行構文へ変換します。
	/// </summary>
	/// <param name="source"></param>
	/// <returns></returns>
	private static string ToCommand(System.Data.IDbCommand source) {
		try {
			return source.CommandText;
		} catch {
			return "Can not get commandText.";
		}
	}
	#endregion 内部メソッド定義
}
