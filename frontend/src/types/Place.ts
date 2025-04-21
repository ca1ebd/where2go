export interface Place {
    id: string;
    address: string;
    googleMapsLink: string;
    appleMapsLink: string;
    description?: string;
    specialInstructions?: string;
    imageUrl?: string;
    shareableUrl: string;
    createdAt: string;
    lastAccessedAt: string;
}

export interface PlaceCreateRequest {
    address: string;
    description?: string;
    specialInstructions?: string;
    imageUrl?: string;
    managementPassword: string;
}

export interface PasswordValidationRequest {
    password: string;
} 