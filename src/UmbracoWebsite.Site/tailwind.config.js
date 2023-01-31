const colors = require('tailwindcss/colors');

module.exports = {
    mode: 'jit',
    content: [
        './Views/**/*.cshtml',
        '.tailwindhack',
    ],
    corePlugins: {
        aspectRatio: false,
    },
    plugins: [
      require('@tailwindcss/typography'),
      require('@tailwindcss/aspect-ratio'),
    ],
    theme: {
        colors: {
            white: '#FEFEFE',
            primary: {
                DEFAULT: '#02B4DD',
            },
            secondary: {
                DEFAULT: '#0077A9',
                dark: '#2C3E50',
                light: '#C5E0FC',
            },
            front: {
                DEFAULT: '#333333',
                faded: '#3F4B5B',
                light: '#5B6E76',
            },
            background: '#F6F7FB',
        },
        fontFamily: {
            sans: ['Open Sans', 'sans-serif'],
        },
        //fontSize: {
        //    sm: ['0.75rem', '1rem'],
        //    base: ['1rem', '1.5rem'],
        //    lg: ['1.3125rem', '1.75rem'],
        //    xl: ['1.75rem', '2rem'],
        //    // Headers
        //    h1: ['5.625rem', '7.625rem'],
        //    h2: ['4.1875rem', '5.75rem'],
        //    h3: ['3.1875rem', '4.3125rem'],
        //    h4: ['2.375rem', '3.25rem'],
        //    h5: ['1.75rem', '2.4375rem'],
        //    // Mobile headers
        //    'm-h1': ['', ''],
        //    'm-h2': ['', ''],
        //    'm-h3': ['', ''],
        //},
      extend: {
        height: {
          'screen-4/5': '80vh',
          'screen-3/5': '60vh',
          'screen-2/5': '40vh',
          'screen-1/2': '50vh'
        },
      },
    },
}
