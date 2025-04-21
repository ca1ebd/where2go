import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { api } from '../services/api';
import { Place } from '../types/Place';

export const ViewPlace: React.FC = () => {
    const { shareableUrl } = useParams<{ shareableUrl: string }>();
    const [place, setPlace] = useState<Place | null>(null);
    const [error, setError] = useState<string>('');
    const [copied, setCopied] = useState<boolean>(false);

    useEffect(() => {
        const fetchPlace = async () => {
            if (!shareableUrl) return;

            try {
                const data = await api.getPlace(shareableUrl);
                setPlace(data);
            } catch (err) {
                setError('Failed to load place details.');
            }
        };

        fetchPlace();
    }, [shareableUrl]);

    const copyToClipboard = (text: string) => {
        navigator.clipboard.writeText(text);
        setCopied(true);
        setTimeout(() => setCopied(false), 2000);
    };

    if (error) {
        return (
            <div className="max-w-2xl mx-auto p-6">
                <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
                    {error}
                </div>
            </div>
        );
    }

    if (!place) {
        return (
            <div className="max-w-2xl mx-auto p-6">
                <div className="animate-pulse">Loading...</div>
            </div>
        );
    }

    return (
        <div className="max-w-2xl mx-auto p-6">
            <div className="bg-white shadow rounded-lg overflow-hidden">
                {place.imageUrl && (
                    <img
                        src={place.imageUrl}
                        alt={place.address}
                        className="w-full h-64 object-cover"
                    />
                )}

                <div className="p-6">
                    <h1 className="text-2xl font-bold mb-4">{place.address}</h1>

                    <div className="space-y-4">
                        <div className="flex space-x-4">
                            <a
                                href={place.googleMapsLink}
                                target="_blank"
                                rel="noopener noreferrer"
                                className="flex-1 bg-blue-600 text-white px-4 py-2 rounded text-center hover:bg-blue-700"
                            >
                                Open in Google Maps
                            </a>
                            <a
                                href={place.appleMapsLink}
                                target="_blank"
                                rel="noopener noreferrer"
                                className="flex-1 bg-gray-800 text-white px-4 py-2 rounded text-center hover:bg-gray-900"
                            >
                                Open in Apple Maps
                            </a>
                        </div>

                        <button
                            onClick={() => copyToClipboard(place.address)}
                            className="w-full bg-gray-100 text-gray-700 px-4 py-2 rounded hover:bg-gray-200"
                        >
                            {copied ? 'Copied!' : 'Copy Address'}
                        </button>

                        {place.description && (
                            <div className="mt-4">
                                <h2 className="text-lg font-semibold mb-2">Description</h2>
                                <p className="text-gray-600">{place.description}</p>
                            </div>
                        )}

                        {place.specialInstructions && (
                            <div className="mt-4">
                                <h2 className="text-lg font-semibold mb-2">Special Instructions</h2>
                                <p className="text-gray-600">{place.specialInstructions}</p>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
}; 