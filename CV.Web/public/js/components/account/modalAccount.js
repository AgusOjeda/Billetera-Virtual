function ModalAccount(account) {  
    let tbodyElments = document.getElementById('modal-section')
    tbodyElments.innerHTML = ''
    tbodyElments.innerHTML += `
        <div id="myModal" class="modal">
            <div class="flex" id="flex">
                <div class="main-modal">
                    <div class="modal-header flex">
                        <h4 class="infoAccount">${account.FullNameCustomer}</h4>
                        <span class="close" id="close"><p>X</p></span>
                    </div>
                    <div class="modal-bodyProduct" id="modal-body">
                        <p class ="top-Header"><b>Balance</b> $${account.Balance} </p>
                        <button class="alias" id="${account.id}" >
                        EDIT
                        </button>
                        <p class="with-border"><b>Alias</b> ${account.Alias} </p> 
                      
                        <p class="with-border"><b>CBU</b> ${account.Cbu} </p>
                        <p class="with-border"><b>CUIL</b> ${account.Cuil} </p>
                    </div>      
                    <div class="footer">
                        <h5 class="footerAccount">GStick &copy;</h5>
                    </div>
                </div>
            </div>
	    </div>   `
    let modal = document.getElementById('myModal')
    modal.style.display = 'block'
    let closeModal = document.getElementById('close')
    
    closeModal.addEventListener('click', function(){
        modal.style.display = 'none'
    }   
    )    
    
    let aliasButton = document.querySelectorAll('.alias')

    for (var i = 0; i < aliasButton.length; i++){
        aliasButton[i].addEventListener('click', ModalAlias)
        aliasButton[i].myParam = aliasButton[i].id
    }  

    function ModalAlias(evt) {
        let accountId = evt.currentTarget.myParam


        window.ChangeAlias(accountId)
    }
    
   
}

export { ModalAccount }