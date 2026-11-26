namespace Otchitta.Library.Common.Service.Dsv;

/// <summary>
/// DSV利用例外情報です。
/// </summary>
public class DsvServiceException : Exception {
	#region メンバー変数定義
	/// <summary>例外理由</summary>
	private readonly string messageText;
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
	/// 引数一覧を取得します。
	/// </summary>
	/// <value>引数一覧</value>
	public ReadOnlyCollection<(string, object?)> ElementList {
		get => this.elementList;
	}
	#endregion プロパティー定義

	#region 生成メソッド定義
	/// <summary>
	/// DSV利用例外情報を生成します。
	/// </summary>
	/// <param name="messageText">例外理由</param>
	/// <param name="elementList">引数配列</param>
	public DsvServiceException(string messageText, params (string, object?)[] elementList) : base(ToMessage(messageText, elementList)) {
		this.messageText = messageText;
		this.elementList = new (elementList);
	}
	#endregion 生成メソッド定義

	private static string ToMessage(string messageText, (string, object?)[] elementList) {
		var result = new StringBuilder();
		result.Append(messageText);
		for (var index = 0; index < elementList.Length; index ++) {
			var choose = elementList[index];
			result.AppendLine();
			result.Append(choose.Item1);
			result.Append('=');
			result.Append(choose.Item2);
		}
		return result.ToString();
	}
}
