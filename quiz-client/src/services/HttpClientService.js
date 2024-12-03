import axios from 'axios';

class HttpClientService {
    constructor() {
        this.client = axios.create();
        this.client.interceptors.request.use(
            (config) => {
                const tokenString = localStorage.getItem('token');
                const accessToken = tokenString ? JSON.parse(tokenString).accessToken : null;

                if (accessToken) {
                    config.headers['Authorization'] = `Bearer ${accessToken}`;
                }
                return config;
            },
            (error) => {
                return Promise.reject(error);
            }
        );
    }

    url(requestParameters) {
        return `${requestParameters.baseUrl || ''}/${requestParameters.controller}${requestParameters.action ? `/${requestParameters.action}` : ''}`;
    }

    async get(requestParameters, id = '') {
        const url = requestParameters.fullEndPoint || `${this.url(requestParameters)}${id ? `/${id}` : ''}${requestParameters.queryString ? `?${requestParameters.queryString}` : ''}`;
        console.log('GET Request URL:', url);

        try {
            const response = await this.client.get(url, { headers: requestParameters.headers });
            return response.data;
        } catch (error) {
            console.error('GET request error:', error);
            throw error;
        }
    }

    async post(requestParameters, body) {
        const url = requestParameters.fullEndPoint || `${this.url(requestParameters)}${requestParameters.queryString ? `?${requestParameters.queryString}` : ''}`;
        console.log('POST Request URL:', url);
        console.log('POST Request Body:', body);

        try {
            const response = await this.client.post(url, body, { headers: requestParameters.headers });
            return response.data;
        } catch (error) {
            console.error('POST request error:', error);
            throw error;
        }
    }

    async put(requestParameters, body) {
        const url = requestParameters.fullEndPoint || `${this.url(requestParameters)}${requestParameters.queryString ? `?${requestParameters.queryString}` : ''}`;
        console.log('PUT Request URL:', url);
        console.log('PUT Request Body:', body);

        try {
            const response = await this.client.put(url, body, { headers: requestParameters.headers });
            return response.data;
        } catch (error) {
            console.error('PUT request error:', error);
            throw error;
        }
    }

    async delete(requestParameters, id) {
        const url = requestParameters.fullEndPoint || `${this.url(requestParameters)}/${id}${requestParameters.queryString ? `?${requestParameters.queryString}` : ''}`;
        console.log('DELETE Request URL:', url);

        try {
            const response = await this.client.delete(url, { headers: requestParameters.headers });
            return response.data;
        } catch (error) {
            console.error('DELETE request error:', error);
            throw error;
        }
    }
}

export default new HttpClientService();
