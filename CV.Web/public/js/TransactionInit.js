import { getParamFromURL } from "./services/URLGetService.js"
import { InitTransactions } from "./container/TransactionsContainer.js"
import { GetJwtToken } from "./services/LocalStorageService.js"

export const Trx = async () => {
    let token = GetJwtToken()

    if(token != null){
        let section = getParamFromURL("section")
        switch(section){
            default:
            case "transactions":
                InitTransactions("1")
                break;
            case "transactions-step-2":
                InitTransactions("2")
                break;
            case "transactions-step-3":
                InitTransactions("3")
                break;
        }
    }
}