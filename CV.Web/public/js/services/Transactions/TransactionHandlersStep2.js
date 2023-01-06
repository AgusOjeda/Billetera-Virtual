import { readFromSS } from "../SessionStorageService.js"
import { newTransaction } from "../TransactionsServices.js"
import { redirectTo } from "../RedirectService.js"
import { trxFailed } from "../../components/Transactions/ModalTrxFailed.js"
import { sideBar } from "../../components/SideBar.js"
import { trxSuccess } from "../../components/Transactions/ModalTrxSuccess.js"

export const initHandlersStep2 = () => {

    const RESULT_CREATED = "201"

    let btnTransf = document.getElementById("btn-transf")

    btnTransf.addEventListener("click", async () => {

        let transaction = readFromSS("step-1-trx")

        let result = await newTransaction(transaction)

        let msg
        
        let btnModal

        let status = result.Status.status

        if (status == RESULT_CREATED) {
            let mainStep2 = document.getElementById("mainStep2")
            mainStep2.remove()
            let _main = document.getElementById("root")
            _main.innerHTML = sideBar()
            _main.insertAdjacentHTML("beforeend", trxSuccess)
            btnModal = document.getElementById("modal-continue")
            transfSuccess(btnModal)
        } else {
            msg = trxFailed(result.Data.message)
            let mainStep2 = document.getElementById("mainStep2")
            mainStep2.remove()
            let _main = document.getElementById("root")
            _main.innerHTML = sideBar()
            _main.insertAdjacentHTML("beforeend", msg)
            btnModal = document.getElementById("modal-continue")
            transfRejected(btnModal)
        }
    })

    document.getElementById("customerName").innerHTML = readFromSS("fullNameCustomer")

    let btnTransfCancel = document.getElementById("btn-transf-cancel")

    btnTransfCancel.addEventListener("click", async () => {
        redirectTo("transactions")
    })

    document.getElementById("side-home").addEventListener("click", () =>{
        redirectTo("home")
    })

    document.getElementById("btn-logout").addEventListener("click", () =>{
        cleanSS()
        cleanLS()
        redirectTo("home")
    })
}

const transfSuccess = (btnModal) => {
    btnModal.addEventListener("click", () => {
            redirectTo("transactions-step-3")
    })
}

const transfRejected = (btnModal) => {
    btnModal.addEventListener("click", () => {
            redirectTo("transactions")
    })
}
