import { resolve } from 'path';
import { defineConfig } from 'vite';

export default defineConfig({
  build: {
    minify: true,
    target: 'modules',
    outDir: './wwwroot/dist',
    sourcemap: true,
    lib: {
      entry: resolve(__dirname, './Assets/Scripts/main.ts'),
      name: 'App',
      formats: ['iife'],
      fileName: (format) => `main.js`,
    },
  },
});
