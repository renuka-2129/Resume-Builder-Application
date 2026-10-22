// API Configuration - ADD THIS AT THE TOP
const API_URL = 'http://localhost:5136/api';

// Initialize when page loads - ADD THIS
document.addEventListener('DOMContentLoaded', function() {
    console.log('Frontend loaded, connecting to backend at:', API_URL);
    testBackendConnection();
    loadResumesFromBackend();
    
    // Add Save to Database button
    addSaveButton();
});

// NEW FUNCTION: Test backend connection
async function testBackendConnection() {
    try {
        const response = await fetch(`${API_URL}/test`);
        const data = await response.json();
        console.log('✅ Backend connected:', data.message);
        showNotification('Connected to backend successfully!', 'success');
    } catch (error) {
        console.error('❌ Backend connection failed:', error);
        showNotification('Cannot connect to backend. Make sure API is running on port 5136!', 'error');
    }
}

// NEW FUNCTION: Show notification
function showNotification(message, type) {
    // Remove any existing notification
    const existingNotification = document.querySelector('.notification');
    if (existingNotification) {
        existingNotification.remove();
    }
    
    const notification = document.createElement('div');
    notification.className = `notification ${type}`;
    notification.textContent = message;
    notification.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        padding: 15px 20px;
        background: ${type === 'success' ? '#4CAF50' : type === 'error' ? '#f44336' : '#2196F3'};
        color: white;
        border-radius: 5px;
        z-index: 1000;
        animation: slideIn 0.5s ease;
        box-shadow: 0 2px 5px rgba(0,0,0,0.2);
    `;
    
    document.body.appendChild(notification);
    
    setTimeout(() => {
        notification.remove();
    }, 3000);
}

// NEW FUNCTION: Add save button to the form
function addSaveButton() {
    const formSection = document.querySelector('.form-section');
    const downloadButton = document.querySelector('button[onclick="downloadPDF()"]');
    
    if (downloadButton) {
        const saveButton = document.createElement('button');
        saveButton.textContent = '💾 Save to Database';
        saveButton.onclick = saveResumeToBackend;
        saveButton.style.marginTop = '10px';
        saveButton.style.background = '#28a745';
        saveButton.style.marginBottom = '10px';
        
        downloadButton.parentNode.insertBefore(saveButton, downloadButton);
    }
}

// NEW FUNCTION: Save resume to backend
async function saveResumeToBackend() {
    // Validate email first
    let email = document.getElementById("email").value;
    let gmailPattern = /^[a-zA-Z0-9._%+-]+@gmail\.com$/;
    
    if (!email) {
        showNotification('Email is required!', 'error');
        return;
    }
    if (!gmailPattern.test(email)) {
        showNotification('Please enter a valid Gmail address!', 'error');
        return;
    }

    const resumeData = {
        fullName: document.getElementById("name").value || "Your Name",
        email: email,
        jobTitle: document.getElementById("job").value || "Job Title",
        professionalSummary: document.getElementById("summary").value || "",
        skills: document.getElementById("skills").value || "",
        projects: document.getElementById("projects").value || "",
        qualifications: document.getElementById("qualifications").value || "",
        references: document.getElementById("references").value || ""
    };

    try {
        showNotification('Saving resume...', 'info');
        
        const response = await fetch(`${API_URL}/resumes`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(resumeData)
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        console.log('✅ Resume saved:', data);
        showNotification(`Resume saved successfully! ID: ${data.id}`, 'success');
        
        // Refresh the resumes list
        loadResumesFromBackend();
        
    } catch (error) {
        console.error('❌ Error saving resume:', error);
        showNotification('Error saving resume. Is backend running on port 5136?', 'error');
    }
}

// NEW FUNCTION: Load all resumes from backend
async function loadResumesFromBackend() {
    try {
        const response = await fetch(`${API_URL}/resumes`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        const resumes = await response.json();
        console.log('✅ Loaded resumes:', resumes);
        displayResumesList(resumes);
        return resumes;
    } catch (error) {
        console.error('❌ Error loading resumes:', error);
        return [];
    }
}

// NEW FUNCTION: Display resumes list in the UI
function displayResumesList(resumes) {
    // Remove existing list if any
    const existingList = document.getElementById('resumesList');
    if (existingList) {
        existingList.remove();
    }

    // Create container for resumes list
    const listContainer = document.createElement('div');
    listContainer.id = 'resumesList';
    listContainer.style.marginTop = '20px';
    listContainer.style.padding = '10px';
    listContainer.style.backgroundColor = '#f8f9fa';
    listContainer.style.borderRadius = '5px';
    
    listContainer.innerHTML = '<h3 style="margin-top:0;">📋 Saved Resumes from Database</h3>';
    
    if (!resumes || resumes.length === 0) {
        listContainer.innerHTML += '<p style="color:#666;">No resumes saved yet. Create one above and click "Save to Database"!</p>';
    } else {
        const table = document.createElement('table');
        table.style.width = '100%';
        table.style.borderCollapse = 'collapse';
        
        // Table header
        table.innerHTML = `
            <thead>
                <tr style="background-color:#007bff; color:white;">
                    <th style="padding:8px; text-align:left;">ID</th>
                    <th style="padding:8px; text-align:left;">Name</th>
                    <th style="padding:8px; text-align:left;">Job Title</th>
                    <th style="padding:8px; text-align:left;">Email</th>
                    <th style="padding:8px; text-align:left;">Actions</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        `;
        
        const tbody = table.querySelector('tbody');
        
        resumes.forEach(resume => {
            const row = document.createElement('tr');
            row.style.borderBottom = '1px solid #ddd';
            row.innerHTML = `
                <td style="padding:8px;">${resume.id}</td>
                <td style="padding:8px;">${resume.fullName}</td>
                <td style="padding:8px;">${resume.jobTitle}</td>
                <td style="padding:8px;">${resume.email}</td>
                <td style="padding:8px;">
                    <button onclick="loadResume(${resume.id})" style="padding:3px 10px; width:auto; background:#28a745; margin-right:5px;">Load</button>
                    <button onclick="deleteResume(${resume.id})" style="padding:3px 10px; width:auto; background:#dc3545;">Delete</button>
                </td>
            `;
            tbody.appendChild(row);
        });
        
        listContainer.appendChild(table);
    }
    
    // Add to preview section
    const previewSection = document.querySelector('.preview-section');
    previewSection.appendChild(listContainer);
}

// NEW FUNCTION: Load a specific resume by ID
async function loadResume(id) {
    try {
        const response = await fetch(`${API_URL}/resumes/${id}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        const resume = await response.json();
        console.log('✅ Loaded resume:', resume);
        
        // Populate form with resume data
        document.getElementById("name").value = resume.fullName || '';
        document.getElementById("email").value = resume.email || '';
        document.getElementById("job").value = resume.jobTitle || '';
        document.getElementById("summary").value = resume.professionalSummary || '';
        document.getElementById("skills").value = resume.skills || '';
        document.getElementById("projects").value = resume.projects || '';
        document.getElementById("qualifications").value = resume.qualifications || '';
        document.getElementById("references").value = resume.references || '';
        
        // Update preview using YOUR existing function
        updatePreview();
        
        showNotification('Resume loaded successfully!', 'success');
    } catch (error) {
        console.error('❌ Error loading resume:', error);
        showNotification('Error loading resume', 'error');
    }
}

// NEW FUNCTION: Delete a resume
async function deleteResume(id) {
    if (!confirm('Are you sure you want to delete this resume?')) {
        return;
    }
    
    try {
        const response = await fetch(`${API_URL}/resumes/${id}`, {
            method: 'DELETE'
        });
        
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        console.log('✅ Resume deleted:', id);
        showNotification('Resume deleted successfully!', 'success');
        
        // Refresh the list
        loadResumesFromBackend();
        
    } catch (error) {
        console.error('❌ Error deleting resume:', error);
        showNotification('Error deleting resume', 'error');
    }
}

// NEW FUNCTION: Search resumes
async function searchResumes(term) {
    try {
        const response = await fetch(`${API_URL}/resumes/search/${term}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        const resumes = await response.json();
        console.log('✅ Search results:', resumes);
        displayResumesList(resumes);
        showNotification(`Found ${resumes.length} resumes`, 'success');
        
    } catch (error) {
        console.error('❌ Error searching resumes:', error);
        showNotification('Error searching resumes', 'error');
    }
}

// =============================================
// YOUR EXISTING FUNCTIONS - NOTHING CHANGED BELOW
// =============================================

function updatePreview() {

    // Gmail Validation
    let email = document.getElementById("email").value;
    let emailMessage = document.getElementById("emailMessage");
    let gmailPattern = /^[a-zA-Z0-9._%+-]+@gmail\.com$/;

    if (email === "") {
        emailMessage.innerText = "Email is required";
        emailMessage.style.color = "red";
    } else if (!gmailPattern.test(email)) {
        emailMessage.innerText = "❌ Invalid Gmail address";
        emailMessage.style.color = "red";
    } else {
        emailMessage.innerText = "✅ Valid Gmail address";
        emailMessage.style.color = "green";
    }

    // Live Preview Update
    document.getElementById("pName").innerText =
        document.getElementById("name").value || "Your Name";

    document.getElementById("pEmail").innerText = email;

    document.getElementById("pJob").innerText =
        document.getElementById("job").value || "Job Title";

    document.getElementById("pSummary").innerText =
        document.getElementById("summary").value;

    // Skills List
    let skillsInput = document.getElementById("skills").value;
    let skillsList = document.getElementById("pSkills");
    skillsList.innerHTML = "";

    if (skillsInput.trim() !== "") {
        skillsInput.split(",").forEach(skill => {
            let li = document.createElement("li");
            li.innerText = skill.trim();
            skillsList.appendChild(li);
        });
    }

    document.getElementById("pProjects").innerText =
        document.getElementById("projects").value;

    document.getElementById("pQualifications").innerText =
        document.getElementById("qualifications").value;

    document.getElementById("pReferences").innerText =
        document.getElementById("references").value;
}


/* PDF DOWNLOAD FUNCTION */
function downloadPDF() {

    const { jsPDF } = window.jspdf;
    const doc = new jsPDF();

    let y = 10;

    doc.setFontSize(18);
    doc.text("Interactive Resume", 10, y);
    y += 10;

    doc.setFontSize(12);
    doc.text("Name: " + document.getElementById("pName").innerText, 10, y);
    y += 8;

    doc.text("Email: " + document.getElementById("pEmail").innerText, 10, y);
    y += 8;

    doc.text("Job Title: " + document.getElementById("pJob").innerText, 10, y);
    y += 10;

    doc.text("Professional Summary:", 10, y);
    y += 8;
    doc.text(document.getElementById("pSummary").innerText, 10, y);
    y += 12;

    doc.text("Skills:", 10, y);
    y += 8;

    let skills = document.querySelectorAll("#pSkills li");
    skills.forEach(skill => {
        doc.text("- " + skill.innerText, 10, y);
        y += 6;
    });

    y += 6;
    doc.text("Projects:", 10, y);
    y += 8;
    doc.text(document.getElementById("pProjects").innerText, 10, y);
    y += 12;

    doc.text("Qualifications:", 10, y);
    y += 8;
    doc.text(document.getElementById("pQualifications").innerText, 10, y);
    y += 12;

    doc.text("References:", 10, y);
    y += 8;
    doc.text(document.getElementById("pReferences").innerText, 10, y);

    doc.save("Resume.pdf");
}

// Add CSS animations
const style = document.createElement('style');
style.textContent = `
    @keyframes slideIn {
        from {
            transform: translateX(100%);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }
    
    .notification {
        font-family: Arial, sans-serif;
        font-size: 14px;
    }
    
    #resumesList button:hover {
        opacity: 0.8;
    }
`;
document.head.appendChild(style);