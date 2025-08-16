import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    server: {
        port: 59627,
        proxy: {
            '/api': {
                target: 'http://localhost:5076',
                changeOrigin: true,
                // Keep the '/api' prefix so backend routes like '/api/articles' match
            }
        }
    }
})
