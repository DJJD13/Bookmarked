import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vitest/config';
import plugin from '@vitejs/plugin-react';
import tailwindcss from "tailwindcss";
import basicSsl from '@vitejs/plugin-basic-ssl';

// https://vitejs.dev/config/
// noinspection JSUnusedGlobalSymbols
export default defineConfig({
    plugins: [plugin(), basicSsl()],
    css: {
        postcss: {
            plugins: [tailwindcss()],
        },
    },
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    test: {
        environment: 'jsdom',
        globals: true,
        setupFiles: './tests/setup.ts'
    }
})
