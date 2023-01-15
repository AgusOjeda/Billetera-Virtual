import { Login } from '../services/AuthenticationServices.js'
import { SaveJwtToken } from '../services/LocalStorageService.js'
window.onload = () => {
  const btnShow = document.querySelector('.bx-show')
  const inputPassword = document.getElementById('password')
  btnShow.addEventListener('click', (e) => {
    if (inputPassword.type === 'password') {
      inputPassword.type = 'text'
      btnShow.classList.remove('bx-show')
      btnShow.classList.add('bx-hide')
    } else {
      inputPassword.type = 'password'
      btnShow.classList.remove('bx-hide')
      btnShow.classList.add('bx-show')
    }
  })
  LoginContainer()
}

const LoginContainer = () => {
  const form = document.getElementById('form')
  form.addEventListener('submit', (e) => {
    e.preventDefault()
    const email = document.getElementById('email').value
    const password = document.getElementById('password').value
    const data = { email, password }
    Login(data, (response) => { SaveJwtToken(response.token)
      window.location.href = '../../public/view/index.html' })
  })
}
