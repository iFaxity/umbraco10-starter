module.exports = (ctx) => ({
  plugins: {
    'postcss-import': {},
    'tailwindcss/nesting': {},
    'tailwindcss': {},
    'autoprefixer': {},
    'postcss-pxtorem': {
      rootValue: 16,
      unitPrecision: 5,
      propList: [
        'margin*',
        'padding*',
        'font',
        'font-size',
        'line-height',
        'letter-spacing',
      ],
    },
  },
});
