import React, { useState } from 'react';

export const FileUpload = () => {
    const [fileName, setFileName] = useState('');
    const [selectedFile, setSelectedFile] = useState(null);
    const [fileNameError, setFileNameError] = useState('');

    const handleFileNameChange = (e) => {
        setFileName(e.target.value);
    };

    const handleFileSelect = (e) => {
        setSelectedFile(e.target.files[0]);
    };

    const handleFormSubmit = (e) => {
        e.preventDefault();

        if (fileName.trim() === '') {
            setFileNameError('File name cannot be empty');
            return;
        }

        if (!selectedFile) {
            setFileNameError('Please select a file');
            return;
        }

        console.log('File Name:', fileName);
        console.log('Selected File:', selectedFile);

        setFileNameError('');
    };

    return (
        <div className='w-full max-w-xs  m-auto justify-center'>
            <h1>FileUpload</h1>
            <form onSubmit={handleFormSubmit}>
                <div>
                    <label
                     class='bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'
                    >File Name:</label>
                    <input
                    className='mb-5 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'
                        type="text"
                        value={fileName}
                        onChange={handleFileNameChange}
                    />
                    <span className="error">{fileNameError}</span>
                </div>
                <div>
                    <label
                    className='bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'
                    >Choose File:</label>
                    <input
                    className='mt-5 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'
                    type="file" onChange={handleFileSelect} />
                </div>
                <div>
                    <button type="submit"  className='mt-5 w-full text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800'>Upload</button>
                </div>
            </form>
        </div>
    );
}
