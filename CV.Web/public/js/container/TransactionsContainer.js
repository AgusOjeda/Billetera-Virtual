import { sideBar } from "../components/SideBar.js"
import { trxStep1 } from "../components/Transactions/TrxStep1.js"
import { trxStep2 } from "../components/Transactions/TrxStep2.js"
import { trxStep3 } from "../components/Transactions/TrxStep3.js"
import { readFromSS } from "../services/SessionStorageService.js"
import { loadDataStep1 } from "../services/Transactions/LoadDataInTransactionView.js"
import { loadDataStep2 } from "../services/Transactions/LoadDataInTransactionView.js"
import { loadDataStep3 } from "../services/Transactions/LoadDataInTransactionView.js"
import { initHandlersStep1 } from "../services/Transactions/TransactionHandlersStep1.js"
import { initHandlersStep2 } from "../services/Transactions/TransactionHandlersStep2.js"
import { initHandlersStep3 } from "../services/Transactions/TransactionHandlersStep3.js"

let _main = document.getElementById("root")

let customerId
let accountId

if (sessionStorage.getItem("customerId") != null)
  customerId = sessionStorage.getItem("customerId")
else
  customerId = "42999AD4-97E4-4E90-9DB1-79D9231A4C0A"

if (readFromSS("accountId") != null)
  accountId = readFromSS("accountId")
else
  accountId = null

export const InitTransactions = (step) => {

  switch (step) {
    case "1":
      _main.insertAdjacentHTML("beforeend", sideBar())
      _main.insertAdjacentHTML("beforeend", trxStep1)
      loadDataStep1(customerId, accountId)
      initHandlersStep1()
      break;
    case "2":
      _main.insertAdjacentHTML("beforeend", sideBar())
      _main.insertAdjacentHTML("beforeend", trxStep2)
      loadDataStep2()
      initHandlersStep2()
      break;
    case "3":
      _main.insertAdjacentHTML("beforeend", sideBar())
      _main.insertAdjacentHTML("beforeend", trxStep3)
      loadDataStep3()
      initHandlersStep3()
  }

  document.getElementById("side-transf").classList.add("bg-bgPrimary", "text-bgSidebar")
}