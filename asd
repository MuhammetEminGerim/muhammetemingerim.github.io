<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Fitness Hareketleri Rehberi</title>
    <!-- Tailwind CSS -->
    <script src="https://cdn.tailwindcss.com"></script>
    <!-- Google Fonts: Inter -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&display=swap" rel="stylesheet">
    <!-- Font Awesome for Icons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css">
    <style>
        body {
            font-family: 'Inter', sans-serif;
            background-color: #111827; /* Koyu gri arka plan */
            color: #f3f4f6;
        }
        /* Kaydırma çubuğu stilleri */
        ::-webkit-scrollbar {
            width: 8px;
        }
        ::-webkit-scrollbar-track {
            background: #1f2937;
        }
        ::-webkit-scrollbar-thumb {
            background: #4b5563;
            border-radius: 10px;
        }
        ::-webkit-scrollbar-thumb:hover {
            background: #6b7280;
        }
        .modal-backdrop {
            backdrop-filter: blur(5px);
        }
        /* Yükleme animasyonu için stil */
        .loader {
            border: 4px solid #f3f3f3;
            border-top: 4px solid #3b82f6;
            border-radius: 50%;
            width: 40px;
            height: 40px;
            animation: spin 1s linear infinite;
        }
        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
    </style>
</head>
<body class="antialiased">

    <div class="container mx-auto p-4 sm:p-6 lg:p-8 relative">
        <!-- Giriş/Kayıt Butonları -->
        <div id="auth-buttons" class="absolute top-4 right-4 sm:top-6 sm:right-6 lg:top-8 lg:right-8 z-10 space-x-2">
            <button id="login-btn" class="px-4 py-2 text-sm font-semibold text-white bg-gray-700/50 backdrop-blur-sm rounded-md hover:bg-gray-600 transition-colors">Giriş Yap</button>
            <button id="register-btn" class="px-4 py-2 text-sm font-semibold text-white bg-blue-600 rounded-md hover:bg-blue-700 transition-colors">Kayıt Ol</button>
        </div>
        <div id="user-info" class="absolute top-4 right-4 sm:top-6 sm:right-6 lg:top-8 lg:right-8 z-10 items-center space-x-3 hidden">
            <span id="user-greeting" class="text-white font-semibold"></span>
            <button id="logout-btn" class="px-4 py-2 text-sm font-semibold text-white bg-red-600 rounded-md hover:bg-red-700 transition-colors">Çıkış Yap</button>
        </div>

        <!-- Başlık ve Açıklama -->
        <header class="text-center mb-8 pt-16 sm:pt-12">
            <h1 class="text-4xl sm:text-5xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-blue-400 to-teal-300">
                FitRehber
            </h1>
            <p class="text-gray-400 mt-2 max-w-2xl mx-auto">
                Öğrenmek istediğiniz hareketi seçin ve doğru formda nasıl yapılacağını keşfedin.
            </p>
        </header>

        <main>
            <!-- Filtreleme Butonları -->
            <div id="filter-buttons" class="flex flex-wrap justify-center gap-2 mb-6">
                <!-- Butonlar JavaScript ile yüklenecek -->
            </div>
            
            <!-- Gemini Program Oluşturucu Butonu -->
            <div class="text-center mb-8">
                <button id="gemini-plan-btn" class="bg-gradient-to-r from-purple-500 to-indigo-500 text-white font-bold py-3 px-6 rounded-full hover:scale-105 transform transition-transform duration-300 shadow-lg shadow-purple-500/30 disabled:opacity-50 disabled:cursor-not-allowed">
                    ✨ Bana Özel Program Oluştur
                </button>
            </div>

            <!-- Hareket Kartları -->
            <div id="exercise-grid" class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
                <!-- Hareket kartları JavaScript ile yüklenecek -->
            </div>
             <p id="no-results" class="text-center text-gray-500 mt-8 text-lg hidden">Bu kategori için henüz hareket eklenmemiş.</p>
        </main>
    </div>

    <!-- Detay Modalı (Popup) -->
    <div id="exercise-modal" class="fixed inset-0 z-50 items-center justify-center p-4 hidden">
        <div id="modal-backdrop" class="fixed inset-0 bg-black bg-opacity-70 modal-backdrop"></div>
        <div id="modal-content" class="bg-gray-800 rounded-2xl shadow-2xl w-full max-w-2xl mx-auto z-10 transform transition-all duration-300 scale-95 opacity-0 overflow-hidden">
            <div class="p-6 sm:p-8 relative">
                <button id="close-modal-btn" class="absolute top-4 right-4 text-gray-400 hover:text-white transition-colors">
                    <i class="fas fa-times fa-2x"></i>
                </button>
                <div class="flex flex-col lg:flex-row gap-8">
                    <!-- GIF Alanı -->
                    <div class="lg:w-1/2 flex-shrink-0">
                         <img id="modal-gif" src="" alt="Hareket animasyonu" class="rounded-lg w-full h-auto object-cover aspect-square bg-gray-700">
                    </div>
                    <!-- Bilgi Alanı -->
                    <div class="lg:w-1/2">
                        <h2 id="modal-title" class="text-3xl font-bold text-white mb-2"></h2>
                        <span id="modal-muscle-group" class="inline-block bg-blue-500 text-white text-sm font-semibold px-3 py-1 rounded-full mb-6"></span>
                        <h3 class="text-xl font-semibold mb-3 text-gray-200 border-b-2 border-gray-700 pb-2">Nasıl Yapılır?</h3>
                        <ul id="modal-instructions" class="space-y-3 text-gray-300 list-disc list-inside">
                            <!-- Talimatlar JavaScript ile eklenecek -->
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Gemini Plan Modalı -->
    <div id="gemini-modal" class="fixed inset-0 z-50 items-center justify-center p-4 hidden">
        <div id="gemini-modal-backdrop" class="fixed inset-0 bg-black bg-opacity-70 modal-backdrop"></div>
        <div id="gemini-modal-content" class="bg-gray-800 rounded-2xl shadow-2xl w-full max-w-2xl mx-auto z-10 transform transition-all duration-300 scale-95 opacity-0 overflow-hidden">
            <div class="p-6 sm:p-8 relative">
                <button id="close-gemini-modal-btn" class="absolute top-4 right-4 text-gray-400 hover:text-white transition-colors">
                    <i class="fas fa-times fa-2x"></i>
                </button>
                <h2 class="text-3xl font-bold text-white mb-4 text-transparent bg-clip-text bg-gradient-to-r from-purple-400 to-indigo-400">✨ Yapay Zeka Antrenman Planı</h2>
                <div id="gemini-plan-container" class="mt-4 text-gray-300 bg-gray-900/50 p-6 rounded-lg min-h-[200px] prose prose-invert prose-p:my-2 prose-ul:my-2">
                    <!-- Gemini cevabı buraya gelecek -->
                </div>
            </div>
        </div>
    </div>

    <!-- Giriş Yapma Modalı -->
    <div id="login-modal" class="fixed inset-0 z-50 items-center justify-center p-4 hidden">
        <div id="login-modal-backdrop" class="fixed inset-0 bg-black bg-opacity-70 modal-backdrop"></div>
        <div id="login-modal-content" class="bg-gray-800 rounded-2xl shadow-2xl w-full max-w-md mx-auto z-10 transform transition-all duration-300 scale-95 opacity-0">
            <div class="p-8">
                <h2 class="text-3xl font-bold text-center text-white mb-6">Giriş Yap</h2>
                <form id="login-form">
                    <div class="mb-4">
                        <label for="login-email" class="block text-gray-300 mb-2">Email</label>
                        <input type="email" id="login-email" class="w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" required>
                    </div>
                    <div class="mb-6">
                        <label for="login-password" class="block text-gray-300 mb-2">Şifre</label>
                        <input type="password" id="login-password" class="w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" required>
                    </div>
                    <button type="submit" class="w-full bg-blue-600 text-white font-bold py-3 rounded-md hover:bg-blue-700 transition-colors">Giriş Yap</button>
                </form>
                <p class="text-center text-gray-400 mt-6">
                    Hesabın yok mu? <a href="#" id="go-to-register" class="text-blue-400 hover:underline">Kayıt Ol</a>
                </p>
            </div>
        </div>
    </div>

    <!-- Kayıt Olma Modalı -->
    <div id="register-modal" class="fixed inset-0 z-50 items-center justify-center p-4 hidden">
        <div id="register-modal-backdrop" class="fixed inset-0 bg-black bg-opacity-70 modal-backdrop"></div>
        <div id="register-modal-content" class="bg-gray-800 rounded-2xl shadow-2xl w-full max-w-md mx-auto z-10 transform transition-all duration-300 scale-95 opacity-0">
            <div class="p-8">
                <h2 class="text-3xl font-bold text-center text-white mb-6">Hesap Oluştur</h2>
                <form id="register-form">
                    <div class="mb-4">
                        <label for="register-name" class="block text-gray-300 mb-2">İsim</label>
                        <input type="text" id="register-name" class="w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" required>
                    </div>
                    <div class="mb-4">
                        <label for="register-email" class="block text-gray-300 mb-2">Email</label>
                        <input type="email" id="register-email" class="w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" required>
                    </div>
                    <div class="mb-6">
                        <label for="register-password" class="block text-gray-300 mb-2">Şifre</label>
                        <input type="password" id="register-password" class="w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500" required>
                    </div>
                    <button type="submit" class="w-full bg-blue-600 text-white font-bold py-3 rounded-md hover:bg-blue-700 transition-colors">Kayıt Ol</button>
                </form>
                <p class="text-center text-gray-400 mt-6">
                    Zaten bir hesabın var mı? <a href="#" id="go-to-login" class="text-blue-400 hover:underline">Giriş Yap</a>
                </p>
            </div>
        </div>
    </div>


    <script>
        // --- ELEMENT REFERENCES ---
        const authButtons = document.getElementById('auth-buttons');
        const userInfo = document.getElementById('user-info');
        const userGreeting = document.getElementById('user-greeting');
        const loginBtn = document.getElementById('login-btn');
        const registerBtn = document.getElementById('register-btn');
        const logoutBtn = document.getElementById('logout-btn');

        const loginModal = document.getElementById('login-modal');
        const loginModalBackdrop = document.getElementById('login-modal-backdrop');
        const loginModalContent = document.getElementById('login-modal-content');
        const loginForm = document.getElementById('login-form');
        const goToRegister = document.getElementById('go-to-register');

        const registerModal = document.getElementById('register-modal');
        const registerModalBackdrop = document.getElementById('register-modal-backdrop');
        const registerModalContent = document.getElementById('register-modal-content');
        const registerForm = document.getElementById('register-form');
        const goToLogin = document.getElementById('go-to-login');
        
        const exerciseGrid = document.getElementById('exercise-grid');
        const filterButtonsContainer = document.getElementById('filter-buttons');
        const noResultsMessage = document.getElementById('no-results');

        const modal = document.getElementById('exercise-modal');
        const modalBackdrop = document.getElementById('modal-backdrop');
        const modalContent = document.getElementById('modal-content');
        const closeModalBtn = document.getElementById('close-modal-btn');

        const geminiPlanBtn = document.getElementById('gemini-plan-btn');
        const geminiModal = document.getElementById('gemini-modal');
        const geminiModalBackdrop = document.getElementById('gemini-modal-backdrop');
        const geminiModalContent = document.getElementById('gemini-modal-content');
        const closeGeminiModalBtn = document.getElementById('close-gemini-modal-btn');
        const geminiPlanContainer = document.getElementById('gemini-plan-container');

        // --- DATA ---
        const exercises = [
            { id: 1, name: 'Şınav (Push-up)', muscleGroup: 'Göğüs', instructions: [ "Yüzüstü pozisyonda, eller omuz genişliğinde açık olacak şekilde başlayın.", "Vücudunuzu düz bir çizgide tutarak göğsünüz yere yaklaşana kadar dirseklerinizi bükün.", "Başlangıç pozisyonuna dönmek için kollarınızla kendinizi itin.", "Hareket boyunca karın ve kalça kaslarınızı sıkı tutun." ], gifUrl: 'https://cdn.jefit.com/assets/img/exercises/gifs/20.gif' },
            { id: 2, name: 'Barfiks (Pull-up)', muscleGroup: 'Sırt', instructions: [ "Bir barı omuz genişliğinden biraz daha geniş bir şekilde avuç içleriniz karşıya bakacak şekilde tutun.", "Kollarınız tamamen açık şekilde asılı kalın.", "Sırt kaslarınızı kullanarak çeneniz barın üzerine gelene kadar kendinizi yukarı çekin.", "Kontrollü bir şekilde başlangıç pozisyonuna geri dönün." ], gifUrl: 'https://cdn.jefit.com/assets/img/exercises/gifs/129.gif' },
            { id: 3, name: 'Squat (Çömelme)', muscleGroup: 'Bacak', instructions: [ "Ayaklarınızı omuz genişliğinde açın, ayak parmaklarınız hafifçe dışarı baksın.", "Sırtınızı düz tutarak kalçanızı geriye ve aşağıya doğru, sandalyeye oturur gibi indirin.", "Uyluklarınız yere paralel olana kadar çömelin.", "Topuklarınızdan güç alarak başlangıç pozisyonuna dönün." ], gifUrl: 'https://cdn.jefit.com/assets/img/exercises/gifs/164.gif' },
            { id: 4, name: 'Mekik (Crunch)', muscleGroup: 'Karın', instructions: [ "Sırtüstü yatın, dizlerinizi bükün ve ayaklarınızı yere düz basın.", "Ellerinizi başınızın arkasına veya göğsünüzün üzerine koyun.", "Karın kaslarınızı sıkarak kürek kemiklerinizi yerden kaldırın.", "Yavaşça başlangıç pozisyonuna dönün. Boynunuzu çekmemeye dikkat edin." ], gifUrl: 'https://cdn.jefit.com/assets/img/exercises/gifs/2.gif' },
            { id: 5, name: 'Dumbbell Omuz Pres', muscleGroup: 'Omuz', instructions: [ "Her iki elinize birer dambıl alarak bir sehpaya oturun.", "Dambılları omuz hizanızda, avuç içleriniz karşıya bakacak şekilde tutun.", "Kollarınız tamamen uzayana kadar dambılları yukarı doğru itin.", "Kontrollü bir şekilde başlangıç pozisyonuna indirin." ], gifUrl: 'https://cdn.jefit.com/assets/img/exercises/gifs/400.gif' },
            { id: 6, name: 'Dumbbell Row', muscleGroup: 'Sırt', instructions: [ "Bir dizinizi ve aynı taraftaki elinizi sehpaya dayayın.", "Diğer elinizle dambılı alın, sırtınız yere paralel olsun.", "Dambılı göğsünüze doğru, sırt kaslarınızı sıkarak çekin.", "Yavaşça başlangıç pozisyonuna indirin ve tekrar edin." ], gifUrl: 'https://cdn.jefit.com/assets/img/exercises/gifs/387.gif' },
            { id: 7, name: 'Lunge', muscleGroup: 'Bacak', instructions: [ "Ayakta dik durun.", "Bir bacağınızla öne doğru büyük bir adım atın.", "Her iki diziniz de 90 derece bükülene kadar vücudunuzu alçaltın. Arka diziniz yere değmemeli.", "Başlangıç pozisyonuna dönmek için öndeki topuğunuzdan güç alın." ], gifUrl: 'https://cdn.jefit.com/assets/img/exercises/gifs/167.gif' },
            { id: 8, name: 'Plank', muscleGroup: 'Karın', instructions: [ "Şınav pozisyonu alın ancak dirseklerinizin ve ön kollarınızın üzerinde durun.", "Vücudunuzu baştan topuklara kadar düz bir çizgide tutun.", "Karın ve kalça kaslarınızı sıkarak pozisyonu koruyun.", "Belinizin aşağı düşmesine izin vermeyin." ], gifUrl: 'https://cdn.jefit.com/assets/img/exercises/gifs/25.gif' },
            { id: 9, name: 'Dumbbell Bench Press', muscleGroup: 'Göğüs', instructions: [ "Düz bir sehpaya sırtüstü uzanın, her elinizde bir dambıl olsun.", "Dambılları göğsünüzün yanlarında, kollarınız bükülü şekilde tutun.", "Kollarınız tamamen uzayana kadar dambılları yukarı doğru itin.", "Kontrollü bir şekilde başlangıç pozisyonuna geri indirin." ], gifUrl: 'https://cdn.jefit.com/assets/img/exercises/gifs/384.gif' }
        ];

        const muscleGroupIcons = {
            'Tümü': 'fa-solid fa-layer-group',
            'Göğüs': 'fa-solid fa-dumbbell',
            'Sırt': 'fa-solid fa-person-running',
            'Bacak': 'fa-solid fa-shoe-prints',
            'Karın': 'fa-solid fa-fire',
            'Omuz': 'fa-solid fa-hand-fist'
        };

        // --- STATE ---
        let activeFilter = 'Tümü';
        let isLoggedIn = false;
        let currentUserName = '';

        // --- INITIALIZATION ---
        window.addEventListener('DOMContentLoaded', () => {
            createFilterButtons();
            displayExercises('Tümü');
            updateAuthStateUI();
        });

        // --- FUNCTIONS ---

        function createFilterButtons() {
            const muscleGroups = ['Tümü', ...new Set(exercises.map(ex => ex.muscleGroup))];
            muscleGroups.forEach(group => {
                const button = document.createElement('button');
                const iconClass = muscleGroupIcons[group] || 'fa-solid fa-question';
                button.innerHTML = `<i class="${iconClass} mr-2 transition-transform duration-300 group-hover:rotate-12"></i><span>${group}</span>`;
                button.dataset.group = group;
                button.className = `group flex items-center justify-center px-5 py-2.5 text-sm font-semibold rounded-full transition-all duration-300 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 transform hover:scale-105`;

                if (group === 'Tümü') {
                    button.classList.add('bg-gradient-to-r', 'from-blue-500', 'to-teal-400', 'text-white', 'shadow-lg', 'shadow-blue-500/30', 'focus:ring-blue-500');
                } else {
                    button.classList.add('bg-gray-700', 'text-gray-300', 'hover:bg-gray-600', 'focus:ring-gray-500');
                }

                button.addEventListener('click', () => {
                    activeFilter = group;
                    updateActiveButton(activeFilter);
                    displayExercises(group);
                });
                filterButtonsContainer.appendChild(button);
            });
        }
        
        function updateActiveButton(activeGroup) {
            const buttons = filterButtonsContainer.querySelectorAll('button');
            buttons.forEach(button => {
                button.classList.remove('bg-gradient-to-r', 'from-blue-500', 'to-teal-400', 'text-white', 'shadow-lg', 'shadow-blue-500/30');
                button.classList.add('bg-gray-700', 'text-gray-300', 'hover:bg-gray-600');
                if (button.dataset.group === activeGroup) {
                    button.classList.remove('bg-gray-700', 'text-gray-300', 'hover:bg-gray-600');
                    button.classList.add('bg-gradient-to-r', 'from-blue-500', 'to-teal-400', 'text-white', 'shadow-lg', 'shadow-blue-500/30');
                }
            });
        }
        
        function displayExercises(muscleGroup) {
            exerciseGrid.innerHTML = '';
            const filteredExercises = muscleGroup === 'Tümü'
                ? exercises
                : exercises.filter(ex => ex.muscleGroup === muscleGroup);
            
            noResultsMessage.classList.toggle('hidden', filteredExercises.length > 0);

            filteredExercises.forEach(exercise => {
                const card = document.createElement('div');
                card.className = 'bg-gray-800 rounded-lg overflow-hidden shadow-lg transform hover:-translate-y-2 transition-transform duration-300 cursor-pointer group';
                card.innerHTML = `
                    <div class="relative">
                        <img src="${exercise.gifUrl}" alt="${exercise.name}" class="w-full h-48 object-cover">
                        <div class="absolute inset-0 bg-black bg-opacity-20 group-hover:bg-opacity-40 transition-all duration-300"></div>
                    </div>
                    <div class="p-4">
                        <h3 class="text-lg font-semibold text-white">${exercise.name}</h3>
                        <p class="text-sm text-blue-400">${exercise.muscleGroup}</p>
                    </div>
                `;
                card.addEventListener('click', () => openModal(exercise));
                exerciseGrid.appendChild(card);
            });
        }
        
        function openModal(exercise) {
            document.getElementById('modal-title').textContent = exercise.name;
            document.getElementById('modal-muscle-group').textContent = exercise.muscleGroup;
            document.getElementById('modal-gif').src = exercise.gifUrl;
            
            const instructionsList = document.getElementById('modal-instructions');
            instructionsList.innerHTML = '';
            exercise.instructions.forEach(step => {
                const li = document.createElement('li');
                li.innerHTML = `<i class="fas fa-check-circle text-blue-400 mr-2"></i>${step}`;
                instructionsList.appendChild(li);
            });

            modal.classList.remove('hidden');
            modal.classList.add('flex');
            setTimeout(() => {
                 modalContent.classList.remove('scale-95', 'opacity-0');
                 modalContent.classList.add('scale-100', 'opacity-100');
            }, 50);
        }

        function closeModal() {
            modalContent.classList.add('scale-95', 'opacity-0');
            modalContent.classList.remove('scale-100', 'opacity-100');
            setTimeout(() => {
                modal.classList.add('hidden');
                modal.classList.remove('flex');
            }, 300);
        }

        function updateAuthStateUI() {
            if (isLoggedIn) {
                authButtons.classList.add('hidden');
                userInfo.classList.remove('hidden');
                userInfo.classList.add('flex');
                userGreeting.textContent = `Hoş geldin, ${currentUserName}!`;
            } else {
                authButtons.classList.remove('hidden');
                userInfo.classList.add('hidden');
                userInfo.classList.remove('flex');
            }
        }

        function openLoginModal() {
            loginModal.classList.remove('hidden');
            loginModal.classList.add('flex');
            setTimeout(() => {
                 loginModalContent.classList.remove('scale-95', 'opacity-0');
            }, 50);
        }

        function closeLoginModal() {
            loginModalContent.classList.add('scale-95', 'opacity-0');
            setTimeout(() => {
                loginModal.classList.add('hidden');
                loginModal.classList.remove('flex');
            }, 300);
        }
        
        function openRegisterModal() {
            registerModal.classList.remove('hidden');
            registerModal.classList.add('flex');
            setTimeout(() => {
                 registerModalContent.classList.remove('scale-95', 'opacity-0');
            }, 50);
        }

        function closeRegisterModal() {
            registerModalContent.classList.add('scale-95', 'opacity-0');
            setTimeout(() => {
                registerModal.classList.add('hidden');
                registerModal.classList.remove('flex');
            }, 300);
        }

        async function generateWorkoutPlan() {
            openGeminiModal();
            geminiPlanContainer.innerHTML = '<div class="flex justify-center items-center h-full"><div class="loader"></div></div>';
            geminiPlanBtn.disabled = true;

            const apiKey = ""; // API anahtarı boş bırakılmalıdır.
            const apiUrl = `https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-preview-05-20:generateContent?key=${apiKey}`;
            
            const muscleGroup = activeFilter === 'Tümü' ? 'tüm vücut' : activeFilter;
            const userQuery = `Bir fitness uzmanı gibi davran. Bana başlangıç seviyesi için bir ${muscleGroup} antrenman programı oluştur. Programda ısınma, ana hareketler (set ve tekrar sayılarıyla) ve soğuma adımları bulunsun. Cevabı HTML formatında, başlıklar için <h3>, listeler için <ul> ve <li> etiketleri kullanarak düzenli bir şekilde hazırla.`;

            const payload = {
                contents: [{ parts: [{ text: userQuery }] }],
            };
            
            try {
                const response = await fetch(apiUrl, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(payload)
                });

                if (!response.ok) {
                    throw new Error(`API hatası: ${response.status}`);
                }

                const result = await response.json();
                const candidate = result.candidates?.[0];

                if (candidate && candidate.content?.parts?.[0]?.text) {
                    const generatedText = candidate.content.parts[0].text;
                    let htmlText = generatedText
                        .replace(/\*\*(.*?)\*\*/g, '<strong>$1</strong>')
                        .replace(/\*(.*?)\*/g, '<em>$1</em>')
                        .replace(/### (.*?)\n/g, '<h3>$1</h3>')
                        .replace(/^- (.*?)\n/gm, '<li>$1</li>')
                        .replace(/(<li>.*<\/li>)/gs, '<ul>$1</ul>');

                    geminiPlanContainer.innerHTML = htmlText;
                } else {
                    geminiPlanContainer.innerHTML = '<p class="text-red-400">Yapay zekadan bir cevap alınamadı. Lütfen daha sonra tekrar deneyin.</p>';
                }

            } catch (error) {
                console.error("Gemini API çağrısı başarısız:", error);
                geminiPlanContainer.innerHTML = `<p class="text-red-400">Bir hata oluştu: ${error.message}. Lütfen internet bağlantınızı kontrol edip tekrar deneyin.</p>`;
            } finally {
                geminiPlanBtn.disabled = false;
            }
        }

        function openGeminiModal() {
            geminiModal.classList.remove('hidden');
            geminiModal.classList.add('flex');
            setTimeout(() => {
                 geminiModalContent.classList.remove('scale-95', 'opacity-0');
                 geminiModalContent.classList.add('scale-100', 'opacity-100');
            }, 50);
        }

        function closeGeminiModal() {
            geminiModalContent.classList.add('scale-95', 'opacity-0');
            geminiModalContent.classList.remove('scale-100', 'opacity-100');
            setTimeout(() => {
                geminiModal.classList.add('hidden');
                geminiModal.classList.remove('flex');
            }, 300);
        }

        // --- EVENT LISTENERS ---
        loginBtn.addEventListener('click', openLoginModal);
        loginModalBackdrop.addEventListener('click', closeLoginModal);
        
        registerBtn.addEventListener('click', openRegisterModal);
        registerModalBackdrop.addEventListener('click', closeRegisterModal);

        goToRegister.addEventListener('click', (e) => {
            e.preventDefault();
            closeLoginModal();
            setTimeout(openRegisterModal, 350);
        });
        
        goToLogin.addEventListener('click', (e) => {
            e.preventDefault();
            closeRegisterModal();
            setTimeout(openLoginModal, 350);
        });

        loginForm.addEventListener('submit', (e) => {
            e.preventDefault();
            currentUserName = document.getElementById('login-email').value.split('@')[0];
            isLoggedIn = true;
            closeLoginModal();
            updateAuthStateUI();
        });

        registerForm.addEventListener('submit', (e) => {
            e.preventDefault();
            currentUserName = document.getElementById('register-name').value;
            isLoggedIn = true;
            closeRegisterModal();
            updateAuthStateUI();
        });

        logoutBtn.addEventListener('click', () => {
            isLoggedIn = false;
            currentUserName = '';
            updateAuthStateUI();
        });

        closeModalBtn.addEventListener('click', closeModal);
        modalBackdrop.addEventListener('click', closeModal);
        document.addEventListener('keydown', (e) => {
            if (e.key === 'Escape' && !modal.classList.contains('hidden')) {
                closeModal();
            }
        });

        geminiPlanBtn.addEventListener('click', generateWorkoutPlan);
        closeGeminiModalBtn.addEventListener('click', closeGeminiModal);
        geminiModalBackdrop.addEventListener('click', closeGeminiModal);
        document.addEventListener('keydown', (e) => {
            if (e.key === 'Escape' && !geminiModal.classList.contains('hidden')) {
                closeGeminiModal();
            }
        });
    </script>

</body>
</html>

