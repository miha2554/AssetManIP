using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssetManip
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			initAssetView();
		}

		// Создает задание добавления актива
		private void btnNew_Click(object sender, EventArgs e)
		{
			// event1.Text = "BTN_NEW";
			if (currentTask != FormTask.None)
				return;

			if (!isTypeSelected && !isMonetaryView)
			{
				assetSubtypeSwitch.SelectedItem = "Предмет";
				isTypeSelected = true;
			}

			switchInputElements(true);

			if (isMonetaryView)
			{
				currentTask = FormTask.CreateMonetaryAsset;
				addMonetaryColumns();
				// assetEdit.
			}
			else
			{
				currentTask = FormTask.CreateNonMonetaryAsset;
				assetSubtypeSwitch.Enabled = true;
				assetSubtypeCallback();
			}
		}
		// Удаляет все отмеченные активы текущего ListView
		private void btnRemove_Click(object sender, EventArgs e)
		{
			var keysRemove = new List<string>();
			if (isMonetaryView)
			{
				for (int i = 0; i < viewMonetaryAssets.Length; ++i)
				{
					// Собираем уникальные названия для удаления
					if (viewMonetaryAssets[i].Checked)
						keysRemove.Add(viewMonetaryAssets[i].SubItems[0].Text);
				}
				foreach (string key in keysRemove)
					MonetaryAssets.Remove(key);
				
				mnrAssetsChanged = true;
			}
			else
			{
				for (int i = 0; i < viewNonMonetaryAssets.Length; ++i)
				{
					// Собираем уникальные названия для удаления
					if (viewNonMonetaryAssets[i].Checked)
						keysRemove.Add(viewNonMonetaryAssets[i].SubItems[0].Text);
				}
				foreach (string key in keysRemove)
					NonMonetaryAssets.Remove(key);
				
				nonmnrAssetsChanged = true;
			}
			updateAssetView();
		}
		// Создает задание редактирования отмеченных активов
		private void btnEdit_Click(object sender, EventArgs e)
		{
			// Если уже есть задание, кнопка не работает
			if (currentTask != FormTask.None)
				return;

			//if (!isTypeSelected && !isMonetaryView)
			//{
			//	assetSubtypeSwitch.SelectedItem = "Предмет";
			//	isTypeSelected = true;
			//}

			switchInputElements(true);

			if (isMonetaryView)
			{
				List<string> editItems = new List<string>();
				foreach (var item in viewMonetaryAssets)
				{
					if (item.Checked)
						editItems.Add(item.SubItems[0].Text);
				}
				if (editItems.Count == 0)
				{
					switchInputElements(false);
					return;
				}

				currentTask = FormTask.EditMonetaryAsset;
				addMonetaryColumns();

				for (int r = 0; r < editItems.Count; ++r)
				{
					// Разрешаем заменить элемент с этим названием
					namesToReplace.Add(MonetaryAssets[editItems[r]].uniqueName);

					assetEdit.Rows.Add();
					assetEdit.Rows[r].Cells[0].Value = MonetaryAssets[editItems[r]].uniqueName;
					assetEdit.Rows[r].Cells[1].Value = MonetaryAssets[editItems[r]].Value.Amount.ToString();
					assetEdit.Rows[r].Cells[2].Value = MonetaryAssets[editItems[r]].Value.Currency;
					assetEdit.Rows[r].Cells[3].Value = MonetaryAssets[editItems[r]].BankName;
					assetEdit.Rows[r].Cells[4].Value = MonetaryAssets[editItems[r]].BankAccount;
					assetEdit.Rows[r].Cells[5].Value = MonetaryAssets[editItems[r]].CustomType;
				}
			}
			else
			{
				List<string> editItems = new List<string>();
				System.Type currentListType = typeof(Nullable);

				foreach (var item in viewNonMonetaryAssets)
				{
					if (item.Checked)
					{
						var key = item.SubItems[0].Text;
						if (NonMonetaryAssets[key].customData.GetType() != currentListType)
						{
							// Разрешено выводить в форму редактирования только одного типа
							if (currentListType == typeof(Nullable))
								currentListType = NonMonetaryAssets[key].customData.GetType();
							else continue;
						}
						editItems.Add(key);
					}
				}
				if (editItems.Count == 0)
				{
					switchInputElements(false);
					return;
				}

				currentTask = FormTask.EditNonMonetaryAsset;
				assetSubtypeSwitch.Enabled = true;

				assetSubtypeSwitch.SelectedItem =
					(currentListType == typeof(AssetTypes.ItemData)) ?
						"Предмет" :
						"Здание";
				// Обновить форму под текущий тип редактируемых активов
				assetSubtypeCallback();

				for (int r = 0; r < editItems.Count; ++r)
				{
					// Разрешаем заменить элемент с этим названием
					namesToReplace.Add(NonMonetaryAssets[editItems[r]].uniqueName);
					
					assetEdit.Rows.Add();
					assetEdit.Rows[r].Cells[0].Value = NonMonetaryAssets[editItems[r]].uniqueName;
					assetEdit.Rows[r].Cells[1].Value = NonMonetaryAssets[editItems[r]].InitialCost.Amount.ToString();
					assetEdit.Rows[r].Cells[2].Value = NonMonetaryAssets[editItems[r]].ResidualCost.Amount.ToString();
					assetEdit.Rows[r].Cells[3].Value = NonMonetaryAssets[editItems[r]].AssessedValue.Amount.ToString();
					assetEdit.Rows[r].Cells[4].Value = NonMonetaryAssets[editItems[r]].AssessedValue.Currency;

					if (currentListType == typeof(AssetTypes.ItemData))
					{
						AssetTypes.ItemData idata = (AssetTypes.ItemData)NonMonetaryAssets[editItems[r]].customData;
						assetEdit.Rows[r].Cells[5].Value = idata.ItemType;
						assetEdit.Rows[r].Cells[6].Value = idata.CreationYear.ToString();
						assetEdit.Rows[r].Cells[7].Value = idata.Amount.ToString();
						assetEdit.Rows[r].Cells[8].Value = idata.Unit;
					}
					else
					{
						AssetTypes.BuildingData bdata = (AssetTypes.BuildingData)NonMonetaryAssets[editItems[r]].customData;
						assetEdit.Rows[r].Cells[5].Value = bdata.BuildingType;
						assetEdit.Rows[r].Cells[6].Value = bdata.ConstructionYear.ToString();
						assetEdit.Rows[r].Cells[7].Value = bdata.InventoryReg.ToString();
						assetEdit.Rows[r].Cells[8].Value = bdata.Address;
					}
				}
			}
		}

		private void btnSwitchType_Click(object sender, EventArgs e)
		{
			// event1.Text = "TYPE_SWITCHED";
			prevIsMonetaryView = isMonetaryView;
			isMonetaryView = !isMonetaryView;
			if (isMonetaryView)
				btnSwitchType.Text = "Вид: Денежные активы";
			else btnSwitchType.Text = "Вид: Неденежные активы";
			updateAssetView();
		}

		private void assetsView_SelectedIndexChanged(object sender, EventArgs e)
		{
		}
		// Кнопка "Сохранить". Применяет сделанные изменения
		private void btnAddFinal_Click(object sender, EventArgs e)
		{
			bool formChanged = false;
			switch (currentTask)
			{
				case FormTask.CreateMonetaryAsset:
				case FormTask.EditMonetaryAsset:
					formChanged = addMonetaryAssetsFromTable(namesToReplace);
					break;
				case FormTask.CreateNonMonetaryAsset:
				case FormTask.EditNonMonetaryAsset:
					formChanged = addNonMonetaryAssetsFromTable(namesToReplace);
					break;
			}

			// Если в первой строке ошибка, не удалять содержимое
			if (!formChanged)
				return;

			namesToReplace.Clear();

			updateAssetView();
			clearTask();
		}
		// Отменяет задание
		private void btnCancel_Click(object sender, EventArgs e)
		{
			clearTask();
		}
		private void assetEdit_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		}
		private void assetSubtypeSwitch_SelectedIndexChanged(object sender, EventArgs e)
		{
			assetSubtypeCallback();
		}

		private void addMonetaryColumns()
		{
			assetEdit.Columns.Add("name", "Название");
			assetEdit.Columns.Add("value", "Стоимость");
			assetEdit.Columns.Add("unit", "Валюта");
			assetEdit.Columns.Add("bank", "Банк");
			assetEdit.Columns.Add("account", "Номер счёта");
			assetEdit.Columns.Add("desc", "Описание");
		}
		private void switchInputElements(bool state)
		{
			assetEdit.Enabled = state;
			btnAddFinal.Enabled = state;
			btnCancel.Enabled = state;
		}
		private void assetSubtypeCallback()
		{
			assetEdit.Columns.Clear();

			assetEdit.Columns.Add("name", "Название");
			assetEdit.Columns.Add("val_beg", "Начальная стоимость");
			assetEdit.Columns.Add("val_res", "Остаточная стоимость");
			assetEdit.Columns.Add("val_asd", "Оценочная стоимость");
			assetEdit.Columns.Add("val_unit", "Валюта");

			if (assetSubtypeSwitch.SelectedItem?.ToString() == "Предмет")
			{
				assetEdit.Columns.Add("type", "Тип объекта");
				assetEdit.Columns.Add("year", "Год создания");
				assetEdit.Columns.Add("amount", "Количество");
				assetEdit.Columns.Add("unit", "Ед. измерения");
			}
			else if (assetSubtypeSwitch.SelectedItem?.ToString() == "Здание")
			{
				assetEdit.Columns.Add("type", "Тип здания");
				assetEdit.Columns.Add("year", "Год постройки");
				assetEdit.Columns.Add("ireg", "Инвентарный номер");
				assetEdit.Columns.Add("addr", "Адрес");
			}
		}
		private void initAssetView()
		{
			// assetsView.View = View.Details;
			assetView.AllowColumnReorder = false;
			assetView.FullRowSelect = true;
			assetView.CheckBoxes = true;
			assetView.GridLines = true;
			assetView.Sorting = SortOrder.Ascending;
			assetView.Scrollable = true;

			// Ставим флаги, чтоб сработала смена вида
			isMonetaryView = true;
			prevIsMonetaryView = false;
			MonetaryAssets = new Dictionary<string, AssetTypes.MonetaryAsset>();
			NonMonetaryAssets = new Dictionary<string, AssetTypes.NonMonetaryAsset>();
			namesToReplace = new HashSet<string>();
			updateAssetView();
		}

		// Обновляет assetsView в зависимости от текущих активов
		private void updateAssetView()
		{
			// Сменить вид на другой тип активов, если необходимо
			if (prevIsMonetaryView != isMonetaryView)
			{
				assetView.Columns.Clear();
				assetView.Columns.Add("Название", 120, HorizontalAlignment.Left);

				if (isMonetaryView)
				{
					assetView.Columns.Add("Стоимость", 120, HorizontalAlignment.Left);
					assetView.Columns.Add("Банк", 120, HorizontalAlignment.Left);
					assetView.Columns.Add("Номер счёта", 120, HorizontalAlignment.Left);
					assetView.Columns.Add("Описание", 1000, HorizontalAlignment.Left);
				}
				else
				{
					assetView.Columns.Add("Нач. стоимость", 120, HorizontalAlignment.Left);
					assetView.Columns.Add("Ост. стоимость", 120, HorizontalAlignment.Left);
					assetView.Columns.Add("Оценочная стоимость", 120, HorizontalAlignment.Left);
					assetView.Columns.Add("Описание", 1000, HorizontalAlignment.Left);
				}
			}

			if (isMonetaryView)
			{
				if (mnrAssetsChanged || prevIsMonetaryView != isMonetaryView)
				{
					assetView.Items.Clear();
					viewMonetaryAssets = new ListViewItem[MonetaryAssets.Count];
					for (int i = 0; i < MonetaryAssets.Keys.Count; ++i)
					{
						viewMonetaryAssets[i] = MonetaryAssets[
							MonetaryAssets.Keys.ElementAt(i)]
							.ToViewItem();
					}
					assetView.Items.AddRange(viewMonetaryAssets);
				}
			}
			else if (nonmnrAssetsChanged || prevIsMonetaryView != isMonetaryView)
			{
				assetView.Items.Clear();
				viewNonMonetaryAssets = new ListViewItem[NonMonetaryAssets.Count];
				for (int i = 0; i < NonMonetaryAssets.Keys.Count; ++i)
				{
					viewNonMonetaryAssets[i] = NonMonetaryAssets[
						NonMonetaryAssets.Keys.ElementAt(i)]
						.ToViewItem();
				}
				assetView.Items.AddRange(viewNonMonetaryAssets);
			}
			prevIsMonetaryView = isMonetaryView;
		}
		private void clearTask()
		{
			assetEdit.Enabled = false;
			btnAddFinal.Enabled = false;
			btnCancel.Enabled = false;
			assetSubtypeSwitch.Enabled = false;
			mnrAssetsChanged = false;
			nonmnrAssetsChanged = false;

			currentTask = FormTask.None;
			assetEdit.Columns.Clear();
		}

		private bool checkVal(bool val, int row)
		{
			if (!val)
				MessageBox.Show("Неверный формат числа в строке " + row, "Ошибка", MessageBoxButtons.OK);
			return val;
		}
		// Возвращает true, если хотя бы один элемент из таблицы был успешно добавлен
		private bool addMonetaryAssetsFromTable(HashSet<string> namesReplace)
		{
			// Если событие изменения активов еще не обработано, ждем
			if (mnrAssetsChanged)
			{
				MessageBox.Show("Событие еще не обработано");
				return true;
			}

			for (int i = 0; i < assetEdit.RowCount; ++i)
			{
				// Считать последнюю строку пустой, если в ней нету названия
				if (i == assetEdit.RowCount - 1 && assetEdit.Rows[i].Cells[0].Value == null)
					break;
				string[] cellValues = new string[assetEdit.Rows[i].Cells.Count];
				for (int j = 0; j < assetEdit.Rows[i].Cells.Count; ++j)
				{
					if (assetEdit.Rows[i].Cells[j].Value == null)
					{
						switch (j)
						{
							// Разрешены активы без описания
							case 5:
							case 3:
								// Если актив без банка, считается, что он в кассе
								cellValues[j] = "";
								break;
							case 4:
								if (cellValues[3] != "")
								{
									MessageBox.Show("Банк указан без счёта в строке " + (i + 1), "Ошибка", MessageBoxButtons.OK);
									return mnrAssetsChanged;
								}
								// Номера счетов без банка скрываются,
								// но при этом должны иметь значения
								cellValues[j] = "-1";
								break;
							default:
								MessageBox.Show("Отсутствует значение в строке " + (i + 1), "Ошибка", MessageBoxButtons.OK);
								return mnrAssetsChanged;
						}
					}
					else cellValues[j] = assetEdit.Rows[i].Cells[j].Value.ToString();
				}

				// Значения массива:
				// 0 - Название
				// 1 - Стоимость
				// 2 - Валюта
				// 3 - Банк
				// 4 - Номер счета
				// 5 - Описание

				if (!decimal.TryParse(cellValues[1], out decimal mValue) ||
					!int.TryParse(cellValues[4], out int bankAcc))
				{
					MessageBox.Show("Неверный формат числа в строке " + (i + 1), "Ошибка", MessageBoxButtons.OK);
					return mnrAssetsChanged;
				}
				var amount = new AssetTypes.Money(mValue, cellValues[2]);
				var obj = new AssetTypes.MonetaryAsset(cellValues[0], amount, cellValues[3], bankAcc, cellValues[5]);

				
				if (MonetaryAssets.ContainsKey(obj.uniqueName))
				{
					if (namesReplace.Contains(obj.uniqueName))
					{
						MonetaryAssets.Remove(obj.uniqueName);
						namesReplace.Remove(obj.uniqueName);
					}
					else
					{
						MessageBox.Show("Элемент с данным названием уже есть в списке", "Ошибка", MessageBoxButtons.OK);
						return mnrAssetsChanged;
					}
				}
				MonetaryAssets.Add(obj.uniqueName, obj);
				mnrAssetsChanged = true;
			}
			foreach (var name in namesReplace)
			{
				if (MonetaryAssets.ContainsKey(name))
				{
					mnrAssetsChanged = true;
					MessageBox.Show("Элемент удален (2)", "Ошибка", MessageBoxButtons.OK);
					MonetaryAssets.Remove(name);
				}
			}
			return mnrAssetsChanged;
		}
		// Возвращает true, если хотя бы один элемент из таблицы был успешно добавлен
		private bool addNonMonetaryAssetsFromTable(HashSet<string> namesReplace)
		{
			// Если событие изменения активов еще не обработано, ждем
			if (nonmnrAssetsChanged)
			{
				MessageBox.Show("Событие еще не обработано");
				return true;
			}
			
			if (assetSubtypeSwitch.SelectedItem == null)
				return false;
			string selItem = assetSubtypeSwitch.SelectedItem.ToString();

			for (int i = 0; i < assetEdit.RowCount; ++i)
			{
				if (i == assetEdit.RowCount - 1 && assetEdit.Rows[i].Cells[0].Value == null)
					break;

				string[] cellValues = new string[assetEdit.Rows[i].Cells.Count];
				for (int j = 0; j < assetEdit.Rows[i].Cells.Count; ++j)
				{
					if (assetEdit.Rows[i].Cells[j].Value == null)
					{
						switch (j)
						{
							case 5: // Тип
							case 8: // Ед. Изм. / Адрес
								cellValues[j] = "";
								break;
							case 6: // Год создания / постройки
							case 7: // Количество / Инв. Номер
								cellValues[j] = "-1";
								break;
							default:
								MessageBox.Show("Отсутствует значение в строке " + (i + 1), "Ошибка", MessageBoxButtons.OK);
								return nonmnrAssetsChanged;
						}
					}
					else cellValues[j] = assetEdit.Rows[i].Cells[j].Value.ToString();
				}

				// Значения массива:
				// 0 - название
				// 1 - нач. стоимость
				// 2 - ост. стоимость
				// 3 - оцен. ст.
				// 4 - валюта
				// =========================
				// Предмет:
				//  5 - тип объекта
				//	6 - год созд.
				//	7 - количество
				//  8 - ед. изм.
				// =========================
				// Здание:
				//	5 - тип здания
				//  6 - год постр.
				//  7 - инв. номер
				//  8 - адрес

				bool successParse = true;
				successParse &= decimal.TryParse(cellValues[1], out decimal initCost);
				successParse &= decimal.TryParse(cellValues[2], out decimal resCost);
				successParse &= decimal.TryParse(cellValues[3], out decimal asdVal);

				if (!checkVal(successParse, i))
					return nonmnrAssetsChanged;

				object customData;
				successParse &= int.TryParse(cellValues[6], out int year);
				if (selItem == "Предмет")
				{
					successParse &= decimal.TryParse(cellValues[7], out decimal amount);
					if (!checkVal(successParse, i))
						return nonmnrAssetsChanged;

					customData = new AssetTypes.ItemData(cellValues[5], year, amount, cellValues[8]);
				}
				else if (selItem == "Здание")
				{
					successParse &= int.TryParse(cellValues[7], out int invReg);
					if (!checkVal(successParse, i))
						return nonmnrAssetsChanged;

					customData = new AssetTypes.BuildingData(cellValues[5], year, invReg, cellValues[8]);
				}
				else
				{
					MessageBox.Show("Не указан тип объекта", "Ошибка", MessageBoxButtons.OK);
					return nonmnrAssetsChanged;
				}

				var obj = new AssetTypes.NonMonetaryAsset(
					cellValues[0],
					new AssetTypes.Money(initCost, cellValues[4]),
					new AssetTypes.Money(resCost, cellValues[4]),
					new AssetTypes.Money(asdVal, cellValues[4]),
					customData
				);

				if (NonMonetaryAssets.ContainsKey(obj.uniqueName))
				{
					if (namesReplace.Contains(obj.uniqueName))
					{
						NonMonetaryAssets.Remove(obj.uniqueName);
						namesReplace.Remove(obj.uniqueName);
					}
					else
					{
						MessageBox.Show("Элемент с данным названием уже есть в списке", "Ошибка", MessageBoxButtons.OK);
						return nonmnrAssetsChanged;
					}
				}
				NonMonetaryAssets.Add(obj.uniqueName, obj);
				nonmnrAssetsChanged = true;
			}
			foreach (var name in namesReplace)
			{
				if (NonMonetaryAssets.ContainsKey(name))
				{
					nonmnrAssetsChanged = true;
					NonMonetaryAssets.Remove(name);
				}
			}
			return nonmnrAssetsChanged;
		}
		

		private enum FormTask
		{
			None = 0,
			CreateMonetaryAsset,
			EditMonetaryAsset,
			CreateNonMonetaryAsset,
			EditNonMonetaryAsset
		}

		public Dictionary<string, AssetTypes.MonetaryAsset> MonetaryAssets;
		public Dictionary<string, AssetTypes.NonMonetaryAsset> NonMonetaryAssets;

		private ListViewItem[] viewMonetaryAssets;
		private bool mnrAssetsChanged;
		private ListViewItem[] viewNonMonetaryAssets;
		private bool nonmnrAssetsChanged;

		private HashSet<string> namesToReplace;

		private bool isMonetaryView;
		private bool prevIsMonetaryView;

		private bool isTypeSelected;

		private FormTask currentTask;
	}
}

namespace AssetTypes
{
	public struct Money
	{
		public Money(decimal amount, string currency)
		{
			Amount = amount;
			Currency = currency;
		}
		public override string ToString()
		{
			return Amount.ToString() + " " + Currency;
		}

		public decimal Amount;
		public string Currency;
	}

	public struct BuildingData
	{
		public BuildingData(string type, int constrYear, int inventoryReg, string address)
		{
			BuildingType = type;
			ConstructionYear = constrYear;
			InventoryReg = inventoryReg;
			Address = address;
		}
		public override string ToString()
		{
			List<string> res = new List<string>();
			if (BuildingType != "")
				res.Add(BuildingType);
			if (ConstructionYear != -1)
				res.Add("год: " + ConstructionYear.ToString());
			if (Address != "")
				res.Add("адрес: " + Address);
			if (InventoryReg != -1)
				res.Add("ИН: " + InventoryReg);

			string result = "";
			for (int i = 0; i < res.Count; ++i)
			{
				result += res[i];
				if (i + 1 < res.Count)
					result += ", ";
			}
			return result;
		}

		public string BuildingType;
		public int ConstructionYear;
		public int InventoryReg;
		public string Address;
	}

	public struct ItemData
	{
		public ItemData(string itemType, int creationYear, decimal amount, string unit)
		{
			ItemType = itemType;
			CreationYear = creationYear;
			Amount = amount;
			Unit = unit;
		}
		public override string ToString()
		{
			List<string> res = new List<string>();
			if (ItemType != "")
				res.Add(ItemType);
			if (Amount != -1.0M)
			{
				res.Add(Amount.ToString() + Unit);
			}
			if (CreationYear != -1)
				res.Add(CreationYear.ToString());

			string result = "";
			for (int i = 0; i < res.Count; ++i)
			{
				result += res[i];
				if (i + 1 < res.Count)
					result += ", ";
			}
			return result;
		}

		public string ItemType;
		public int CreationYear;
		public decimal Amount;
		public string Unit;
	}

	public class MonetaryAsset
	{
		public MonetaryAsset()
		{
		}
		public MonetaryAsset(string id, Money value, string bankName = "", int bankAccount = 0, string customType = "")
		{
			uniqueName = id;
			Value = value;
			BankName = bankName;
			BankAccount = bankAccount;
			CustomType = customType;
		}

		// Преобразует объект в читаемый вид
		public ListViewItem ToViewItem()
		{
			ListViewItem item = new ListViewItem(uniqueName, 0);
			bool isInBank = BankName != "";
			item.SubItems.Add(Value.ToString());
			item.SubItems.Add(isInBank ? BankName : "(В кассе)");
			item.SubItems.Add(isInBank ? BankAccount.ToString() : "");
			item.SubItems.Add(CustomType);
			return item;
		}

		public string uniqueName; // напр. "перевод_11"
		public Money Value;

		public string BankName; // если строка пустая, то деньги в кассе
		public int BankAccount; // номер счета

		public string CustomType; // напр. "Талоны на бензин". если строка пустая, то чистыми.
	}

	public class NonMonetaryAsset
	{
		public NonMonetaryAsset()
		{
		}
		public NonMonetaryAsset(string id, Money initialCost, Money residualCost, Money assessedValue, object data)
		{
			if (data.GetType() != typeof(ItemData)
			&& data.GetType() != typeof(BuildingData))
				throw new ArgumentException("Invalid parameter", nameof(data));

			uniqueName = id;
			InitialCost = initialCost;
			ResidualCost = residualCost;
			AssessedValue = assessedValue;
			customData = data;
		}

		public ListViewItem ToViewItem()
		{
			ListViewItem item = new ListViewItem(uniqueName, 0);
			item.SubItems.Add(InitialCost.ToString());
			item.SubItems.Add(ResidualCost.ToString());
			item.SubItems.Add(AssessedValue.ToString());
			item.SubItems.Add(customData.ToString());
			return item;
		}

		public string uniqueName; // напр. "гвозди_1"
		public object customData;

		public Money InitialCost;
		public Money ResidualCost;
		public Money AssessedValue;
	}
}
