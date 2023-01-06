function TableHistoryTransactions() {  
    let tbodyElments = document.getElementById('tableHistory-section')
    tbodyElments.innerHTML += `
    <table class="historyAccount-table">
        <thead class="table-header">
        <tr>
            <th class="itemLeft">From</th>
            <th class="header__item">CBU</th>
            <th class="header__item">To</th>
            <th class="header__item">CBU</th>
            <th class="header__item">Amount</th>
            <th class="header__item">Type Operation</th>
            <th class="header__item">Date </th>
            <th class="itemRight">State</th>
         
        </tr>
        </thead>
    <tbody id="history-table-id" class="table-content"></tbody>
    </table>   `
}


export { TableHistoryTransactions }