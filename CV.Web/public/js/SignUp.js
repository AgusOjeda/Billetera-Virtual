import { SignUpStep2 } from './components/signUp/SignUpStep2.js'
import { SignUpStep1 } from './components/signUp/SignUpStep1.js'
import { SignUpStep3 } from './components/signUp/SignUpStep3.js'
import { SignUpFase2 } from './components/signUp/signUpFase2.js'
import { SignUpFase3 } from './components/signUp/signUpFase3.js'
import { SignUpCongratulations } from './components/signUp/signUpCongratulations.js'
import { SignUp, VerifyEmailCode, ChangeState } from '../js/services/AuthenticationServices.js'
import { CreateCustomer, PostAddress, SetVerification } from '../js/services/OnBoardingService.js'
import { SaveJwtToken } from '../js/services/LocalStorageService.js'
import { CreateAccount, CreateAccount2, UpdateBalance } from '../js/services/AccountServices.js'
import { loadIntoSS } from './services/SessionStorageService.js'

let _root
const validExtensions = ['image/jpeg', 'image/jpg', 'image/png']

window.onload = () => {
  _root = document.getElementById('root')
  FirstStep()
  const btnShow = document.getElementsByClassName('bx-show')
  for (const btn of btnShow) {
    btn.addEventListener('click', (e) => {
      const inputPassword = e.target.previousElementSibling
      if (inputPassword.type === 'password') {
        inputPassword.type = 'text'
        btn.classList.remove('bx-show')
        btn.classList.add('bx-hide')
      } else {
        inputPassword.type = 'password'
        btn.classList.remove('bx-hide')
        btn.classList.add('bx-show')
      }
    })
  }
}
let validPassword = false
const MatchPassword = () => {
  const password = document.getElementById('password')
  const confirmPassword = document.getElementById('confirm-password')
  const error = document.getElementById('error')
  confirmPassword.addEventListener('keyup', () => {
    const error = document.getElementById('error')
    if (password.value !== confirmPassword.value) {
      error.textContent = 'Las contraseñas no coinciden'
      validPassword = false
    } else {
      error.textContent = ''
      validPassword = true
    }
  })
}

const FirstStep = () => {
  _root.innerHTML = ''
  _root.innerHTML = SignUpStep1()
  const form = document.getElementById('form')
  MatchPassword()
  form.addEventListener('submit', async (e) => {
    e.preventDefault()
    const email = document.getElementById('email').value
    const password = document.getElementById('password').value
    const check = document.getElementById('terms').checked
    if (validPassword && check) {
      const data = {
        email,
        password
      }
      await SignUp(data, (body) => {
        SaveJwtToken(body.token)
      })
      SecondStep(data)
    } else {
      console.log('No se pudo enviar')
    }
  })
}
const SecondStep = (data) => {
  _root.innerHTML = ''
  _root.innerHTML = SignUpStep2(data)
  const inputElements = [...document.querySelectorAll('input.code-input')]

  inputElements.forEach((ele, index) => {
    ele.addEventListener('keydown', (e) => {
      // if the keycode is backspace & the current field is empty
      // focus the input before the current. Then the event happens
      // which will clear the "before" input box.
      if (e.keyCode === 8 && e.target.value === '') inputElements[Math.max(0, index - 1)].focus()
    })
    ele.addEventListener('input', (e) => {
      // take the first character of the input
      const [first, ...rest] = e.target.value
      e.target.value = first ?? ''
      const lastInputBox = index === inputElements.length - 1
      const didInsertContent = first !== undefined
      if (didInsertContent && !lastInputBox) {
        // continue to input the rest of the string
        inputElements[index + 1].focus()
        inputElements[index + 1].value = rest.join('')
        inputElements[index + 1].dispatchEvent(new Event('input'))
      }
    })
  })

  // Get code data
  const VerifyBtn = document.getElementById('verifyBtn')
  VerifyBtn.addEventListener('click', async (e) => {
    e.preventDefault()
    const code = inputElements.map(({ value }) => value).join('')
    await VerifyEmailCode(code)
    /*
    if (!verifyCode) {
      const text = document.getElementById('wrongCode')
      text.textContent = 'El código es incorrecto'
      inputElements.forEach((ele) => {
        ele.value = ''
      })
      inputElements[0].focus()
    } else {
    } */
    ThirdStep()
  })
}

const ThirdStep = () => {
  _root.innerHTML = ''
  _root.innerHTML = SignUpStep3()
  const form = document.getElementById('form')
  form.addEventListener('submit', (e) => {
    e.preventDefault()
    const firstName = document.getElementById('firstName').value
    const lastName = document.getElementById('lastName').value
    const phone = document.getElementById('tel').value
    const dni = document.getElementById('dni').value
    const cuil = document.getElementById('CUIL').value
    const data = {
      FirstName: firstName,
      LastName: lastName,
      Dni: dni,
      Cuil: cuil,
      Phone: phone
    }
    CreateCustomer(data, () => {})
    ThirdStepFase2(data)
  })
}

const ThirdStepFase2 = (data) => {
  const body = document.getElementById('step')
  body.innerHTML = ''
  body.innerHTML = SignUpFase2(data)
  const form = document.getElementById('form')
  form.addEventListener('submit', (e) => {
    e.preventDefault()
    const street = document.getElementById('street').value
    const number = document.getElementById('number').value
    const location = document.getElementById('location').value
    const province = document.getElementById('province-select').value
    const data = {
      Street: street,
      Number: number,
      Location: location,
      Province: province
    }
    PostAddress(data, () => {})
    ThirdStepFase3()
  })
}

const ThirdStepFase3 = () => {
  const body = document.getElementById('step')
  body.innerHTML = ''
  body.innerHTML = SignUpFase3()
  const dragArea = document.getElementById('uploadAreaFront')
  const dragAreaBack = document.getElementById('uploadAreaBack')
  const btnUploadFront = document.getElementById('btnUploadFront')
  const inputFront = document.getElementById('fileFront')
  const btnUploadBack = document.getElementById('btnUploadBack')
  const inputBack = document.getElementById('fileBack')
  const btnContinue = document.getElementById('btnContinue')
  btnContinue.addEventListener('click', () => {
    SetVerification(() => {})
    const data = {
      state: 1
    }
    ChangeState(data, () => {})
    CreateAccount2("ARS")
    ThirdStepFase4()
  })
  // INPUT FILE FRONT
  btnUploadFront.addEventListener('click', () => {
    inputFront.click()
  })

  inputFront.addEventListener('change', () => {
    const file = inputFront.files[0]
    if (validExtensions.includes(file.type)) {
      ShowFile(file, dragArea)
    } else {
      const message = document.getElementById('supportBack')
      message.textContent = 'Solo se permiten archivos JPEG, JPG, PNG'
      message.classList.add('text-red-500')
    }
  })
  // INPUT FILE BACK
  btnUploadBack.addEventListener('click', () => {
    inputBack.click()
  })

  inputBack.addEventListener('change', () => {
    const file = inputBack.files[0]
    if (validExtensions.includes(file.type)) {
      ShowFile(file, dragAreaBack)
    } else {
      const message = document.getElementById('supportBack')
      message.textContent = 'Solo se permiten archivos JPEG, JPG, PNG'
      message.classList.add('text-red-500')
    }
  })

  DragAndDrop(dragArea, 'front')
  DragAndDrop(dragAreaBack, 'back')
}

const DragAndDrop = (dragArea, opt) => {
  dragArea.addEventListener('dragover', (event) => {
    event.preventDefault()
    dragArea.classList.remove('border-dashed')
  })
  dragArea.addEventListener('dragleave', () => {
    dragArea.classList.add('border-dashed')
  })
  dragArea.addEventListener('drop', (event) => {
    event.preventDefault()
    const file = event.dataTransfer.files[0]
    if (validExtensions.includes(file.type)) {
      ShowFile(file, dragArea)
    } else {
      let message
      if (opt === 'front') {
        message = document.getElementById('supportFront')
      } else {
        message = document.getElementById('supportBack')
      }
      message.textContent = 'Solo se permiten archivos JPEG, JPG, PNG'
      message.classList.add('text-red-500')
    }
  })
}
const ShowFile = (file, dragArea) => {
  const reader = new window.FileReader()
  reader.readAsDataURL(file)
  reader.onload = function () {
    const result = reader.result
    const img = document.createElement('img')
    img.src = result
    img.width = 300
    img.height = 300
    img.classList.add('object-cover', 'rounded', 'pointer-events-none')
    const message = document.createElement('p')
    message.id = 'firstMessageFront'
    message.classList.add('font-semibold', 'text-2xl', 'mt-2', 'mb-2')
    message.textContent = 'Documento recibido con exito'
    dragArea.innerHTML = ''
    dragArea.appendChild(img)
    dragArea.appendChild(message)
  }
}

const ThirdStepFase4 = async () => {
  const body = document.getElementById('inputdata')
  body.innerHTML = SignUpCongratulations()
  const state = document.getElementById('state')
  state.classList.remove('bg-[#e9e8fc]', 'text-[#6e6eff]')
  state.classList.add('bg-[#20BF55]', 'text-white')
  state.innerText = 'Verificado'
  const counter = document.getElementById('contador')
  // make counter count down from 5 to 0

  let count = 5
  const interval = setInterval(() => {
    counter.textContent = count
    count--
    if (count < 0) {
      clearInterval(interval)
      redirectToAccount()
    }
  }, 1000)
}

const redirectToAccount = () => {
  window.location.href = '../../public/view/index.html'
}
