import React, { useState } from 'react';

const FileUpload = () => {
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

        // Validation: Check if the file name is not empty
        if (fileName.trim() === '') {
            setFileNameError('File name cannot be empty');
            return;
        }

        // Validation: Check if a file is selected
        if (!selectedFile) {
            setFileNameError('Please select a file');
            return;
        }

        console.log('File Name:', fileName);
        console.log('Selected File:', selectedFile);

        setFileNameError('');
    };

    return (
        <div>
            <h1>FileUpload</h1>
            <form onSubmit={handleFormSubmit}>
                <div>
                    <label>File Name:</label>
                    <input
                        type="text"
                        value={fileName}
                        onChange={handleFileNameChange}
                    />
                    <span className="error">{fileNameError}</span>
                </div>
                <div>
                    <label>Choose File:</label>
                    <input type="file" onChange={handleFileSelect} />
                </div>
                <div>
                    <button type="submit">Upload</button>
                </div>
            </form>
        </div>
    );
}

export default FileUpload;

