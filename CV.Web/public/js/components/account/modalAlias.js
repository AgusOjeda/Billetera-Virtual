function ModalAlias(alias, id) {  
    let tbodyElments = document.getElementById('modal-section')
    tbodyElments.innerHTML = ''
    tbodyElments.innerHTML += `
        <div id="myModal" class="modal">
            <div class="flex" id="flex">
                <div class="main-modal">
                    <div class="modal-header flex">
                        <h4 class="infoAccount"> Update Alias</h4>
                        <span class="close" id="close"><p>X</p></span>
                    </div>
                    <div class="modal-bodyProduct" id="modal-body">
                        <p class ="top-header"><b>Alias</b></p>
                        <input class="flexsearch--input" id="inputAlias" value="${alias}">
                        <div class="div-update"><button class="alias2" id="${id}" >
                        UPDATE
                        </button></div>
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
    
    let aliasButton = document.querySelectorAll('.alias2')

    for (var i = 0; i < aliasButton.length; i++){
        aliasButton[i].addEventListener('click', ModalAlias)
        aliasButton[i].myParam = aliasButton[i].id
    }  

    async function ModalAlias(evt) {
        let alias = evt.currentTarget.myParam           
        let  inputValue = document.getElementById('inputAlias').value        
        let resp = await window.SetAlias(alias, inputValue)
        if (resp != null) {
            alert('Alias changed successfully.')
            modal.style.display = 'none'
        }
        if (resp == null) {
            alert('An error occurred. Please, try it again.')
        }
    
    }
    
   
}

export { ModalAlias }