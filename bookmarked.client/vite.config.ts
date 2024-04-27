import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vitest/config';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import /* child_process */ { execSync } from 'child_process';
// import { env } from 'process';
import tailwindcss from "tailwindcss";

// const baseFolder =
//     env.APPDATA !== undefined && env.APPDATA !== ''
//         ? `${env.APPDATA}/ASP.NET/https`
//         : `${env.HOME}/.aspnet/https`;
//
// const certificateName = "bookmarked.client";
// const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
// const keyFilePath = path.join(baseFolder, `${certificateName}.key`);
//
// if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
//     if (0 !== child_process.spawnSync('dotnet', [
//         'dev-certs',
//         'https',
//         '--export-path',
//         certFilePath,
//         '--format',
//         'Pem',
//         '--no-password',
//     ], { stdio: 'inherit', }).status) {
//         throw new Error("Could not create certificate.");
//     }
// }
//
// const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
//     env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7170';
//
// https://vitejs.dev/config/
// noinspection JSUnusedGlobalSymbols
export default defineConfig({
    plugins: [plugin()],
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
    server: {
        port: 5173,
        https: generateCerts() 
    },
    test: {
        environment: 'jsdom',
        globals: true,
        setupFiles: './tests/setup.ts'
    }
})

function generateCerts() {
    const baseFolder =
        process.env.APPDATA !== undefined && process.env.APPDATA !== ""
            ? `${process.env.APPDATA}/ASP.NET/https`
            : `${process.env.HOME}/.aspnet/https`;
    const certificateArg = process.argv
        .map((arg) => arg.match(/--name=(?<value>.+)/i))
        .filter(Boolean)[0];
    const certificateName = certificateArg
        ? certificateArg.groups!.value
        : process.env.npm_package_name;

    if (!certificateName) {
        console.error(
            "Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly."
        );
        process.exit(-1);
    }

    const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
    const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

    if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
        const outp = execSync(
            "dotnet " +
            [
                "dev-certs",
                "https",
                "--export-path",
                certFilePath,
                "--format",
                "Pem",
                "--no-password",
            ].join(" ")
        );
        console.log(outp.toString());
    }

    return {
        cert: fs.readFileSync(certFilePath, "utf8"),
        key: fs.readFileSync(keyFilePath, "utf8"),
    };
}