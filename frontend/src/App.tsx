import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { CreatePlace } from './components/CreatePlace';
import { ViewPlace } from './components/ViewPlace';

function App() {
  return (
    <Router>
      <div className="min-h-screen bg-gray-50">
        <nav className="bg-white shadow-sm">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div className="flex justify-between h-16">
              <div className="flex">
                <div className="flex-shrink-0 flex items-center">
                  <Link to="/" className="text-xl font-bold text-indigo-600">
                    Where2Go
                  </Link>
                </div>
              </div>
              <div className="flex items-center">
                <Link
                  to="/create"
                  className="ml-8 inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700"
                >
                  Create Place
                </Link>
              </div>
            </div>
          </div>
        </nav>

        <main className="py-10">
          <Routes>
            <Route path="/create" element={<CreatePlace />} />
            <Route path="/place/:shareableUrl" element={<ViewPlace />} />
            <Route path="/" element={
              <div className="max-w-2xl mx-auto p-6 text-center">
                <h1 className="text-4xl font-bold text-gray-900 mb-4">
                  Share Places with Friends
                </h1>
                <p className="text-xl text-gray-600 mb-8">
                  Create a shareable link with Google Maps and Apple Maps integration.
                </p>
                <Link
                  to="/create"
                  className="inline-flex items-center px-6 py-3 border border-transparent text-base font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700"
                >
                  Get Started
                </Link>
              </div>
            } />
          </Routes>
        </main>
      </div>
    </Router>
  );
}

export default App;
