import { Place, PlaceCreateRequest, PasswordValidationRequest } from '../types/Place';

const API_BASE_URL = 'http://localhost:5000/api';

export const api = {
    async createPlace(request: PlaceCreateRequest): Promise<Place> {
        const response = await fetch(`${API_BASE_URL}/places`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(request),
        });

        if (!response.ok) {
            throw new Error('Failed to create place');
        }

        return response.json();
    },

    async getPlace(shareableUrl: string): Promise<Place> {
        const response = await fetch(`${API_BASE_URL}/places/${shareableUrl}`);

        if (!response.ok) {
            throw new Error('Failed to get place');
        }

        return response.json();
    },

    async validatePassword(shareableUrl: string, request: PasswordValidationRequest): Promise<boolean> {
        const response = await fetch(`${API_BASE_URL}/places/${shareableUrl}/validate`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(request),
        });

        if (!response.ok) {
            throw new Error('Failed to validate password');
        }

        return response.json();
    },
}; 