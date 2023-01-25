import api from './interceptor';

export const get = async <T>(endpoint: string): Promise<T> => {
    const response = await api.get<T>(endpoint);
    return response.data;
};

export const post = async <T>(endpoint: string, data?: any): Promise<T> => {
    const response = await api.post<T>(endpoint, data);
    return response.data;
};

export const put = async <T>(endpoint: string, data?: any): Promise<T> => {
    const response = await api.put<T>(endpoint, data);
    return response.data;
};

export const remove = async <T>(endpoint: string): Promise<T> => {
    const response = await api.delete<T>(endpoint);
    return response.data;
};