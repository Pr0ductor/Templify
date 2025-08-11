document.addEventListener('DOMContentLoaded', function() {
  const burger = document.querySelector('.header__burger');
  const sidebar = document.querySelector('.sidebar');
  const overlay = document.querySelector('.overlay');

  // --- sidebar logic ---
  function openSidebar() {
    sidebar.classList.add('open');
    overlay.classList.add('active');
    document.body.style.overflow = 'hidden';
  }
  function closeSidebar() {
    sidebar.classList.remove('open');
    overlay.classList.remove('active');
    document.body.style.overflow = '';
  }
  burger && burger.addEventListener('click', openSidebar);
  overlay && overlay.addEventListener('click', closeSidebar);
  window.addEventListener('resize', function() {
    if(window.innerWidth >= 1200) closeSidebar();
  });
  // Закрытие по Esc
  document.addEventListener('keydown', function(e) {
    if(e.key === 'Escape') closeSidebar();
  });

  // --- Account dropdown logic (header) ---
  document.querySelectorAll('.header__account-dropdown').forEach(function(dropdown) {
    const btn = dropdown.querySelector('.header__account');
    const menu = dropdown.querySelector('.header__account-menu');
    if (btn && menu) {
      btn.addEventListener('click', function(e) {
        e.stopPropagation();
        menu.classList.toggle('open');
      });
      document.addEventListener('click', function(e) {
        if (!dropdown.contains(e.target)) {
          menu.classList.remove('open');
        }
      });
      // Переходы по кнопкам
      const signInBtn = menu.querySelector('.header__account-menu-btn:nth-child(1)');
      const signUpBtn = menu.querySelector('.header__account-menu-btn:nth-child(2)');
      if (signInBtn) {
        signInBtn.addEventListener('click', function() {
          window.location.href = 'auth.html';
        });
      }
      if (signUpBtn) {
        signUpBtn.addEventListener('click', function() {
          window.location.href = 'register.html';
        });
      }
    }
  });

  // --- Account dropdown logic (sidebar) ---
  document.querySelectorAll('.sidebar__account-dropdown').forEach(function(dropdown) {
    const btn = dropdown.querySelector('.sidebar__account');
    const menu = dropdown.querySelector('.sidebar__account-menu');
    if (btn && menu) {
      btn.addEventListener('click', function(e) {
        e.stopPropagation();
        menu.classList.toggle('open');
      });
      document.addEventListener('click', function(e) {
        if (!dropdown.contains(e.target)) {
          menu.classList.remove('open');
        }
      });
      // Переходы по кнопкам
      const signInBtn = menu.querySelector('.sidebar__account-menu-btn:nth-child(1)');
      const signUpBtn = menu.querySelector('.sidebar__account-menu-btn:nth-child(2)');
      if (signInBtn) {
        signInBtn.addEventListener('click', function() {
          window.location.href = 'auth.html';
        });
      }
      if (signUpBtn) {
        signUpBtn.addEventListener('click', function() {
          window.location.href = 'register.html';
        });
      }
    }
  });
}); 