import { redirectTo } from "../RedirectService.js"
import { loadDataStep3 } from "./LoadDataInTransactionView.js"

export const initHandlersStep3 = () => {

    loadDataStep3()

    let btnContinue = document.getElementById("btn-continue")

    btnContinue.addEventListener("click", async () => {
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