namespace Otchitta.Library.Matrix;

/// <summary>
/// 読込格子基本情報です。
/// </summary>
public interface ReadableMatrixData {
	/// <summary>
	/// 表題一覧を取得します。
	/// </summary>
	/// <returns>表題一覧</returns>
	public ReadableColumnList ColumnList {
		get;
	}
	/// <summary>
	/// 要素一覧を取得します。
	/// </summary>
	/// <returns>表題一覧</returns>
	public ReadableRecordList RecordList {
		get;
	}

	/// <summary>
	/// 表題個数を取得します。
	/// </summary>
	/// <returns>表題個数</returns>
	public int ColumnSize {
		get => ColumnList.Count;
	}
	/// <summary>
	/// 要素個数を取得します。
	/// </summary>
	/// <returns>要素個数</returns>
	public int RecordSize {
		get => RecordList.Count;
	}

	/// <summary>
	/// 項目情報を取得します。
	/// </summary>
	/// <param name="recordIndex">要素番号</param>
	/// <param name="columnIndex">表題番号</param>
	/// <returns>項目情報</returns>
	public object? this[int recordIndex, int columnIndex] {
		get => GetData(recordIndex, columnIndex);
	}

	/// <summary>
	/// 項目情報を取得します。
	/// </summary>
	/// <param name="recordIndex">要素番号</param>
	/// <param name="columnIndex">表題番号</param>
	/// <returns>項目情報</returns>
	private object? GetData(int recordIndex, int columnIndex) {
		if (recordIndex < 0) {
			throw new ArgumentOutOfRangeException(nameof(recordIndex), recordIndex, "Must be non negative.");
		} else if (RecordSize <= recordIndex) {
			throw new ArgumentOutOfRangeException(nameof(recordIndex), recordIndex, "This over recordSize.");
		} else if (columnIndex < 0) {
			throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, "Must be non negative.");
		} else if (ColumnSize <= columnIndex) {
			throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, "This over columnSize.");
		} else {
			var recordData = RecordList[recordIndex];
			return columnIndex < recordData.Count? null: recordData[columnIndex];
		}
	}
}
