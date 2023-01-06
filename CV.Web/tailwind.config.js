/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './public/view/*.{html,js}',
    './public/js/components/*.js',
    './public/js/components/**/*.js',
    './public/js/container/*.js'
  ],
  theme: {
    container: {
      center: true
    },
    extend: {
      fontFamily: {
        GilroyRegular: ['Gilroy-Regular', 'sans-serif']
      },
      colors: {
        bgPrimary: "#EFF1FA",
        textColor: "#1A2030",
        secondary: "#1D5BFB",
        secondaryHover: "#174ACE",
        gold: "#FBC920",
        bgSidebar: "#1A2030",
        brand: "#5E8BFF"
      },
      backgroundImage: {
        registerBg3: "url('../../public/images/bg.png')"
      }
    },
  },
  plugins: [require('@tailwindcss/forms')]
}
