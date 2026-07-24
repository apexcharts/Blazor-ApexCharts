window.docsInterop = {
    scrollToFragment: function (id) {
        var el = document.getElementById(id);
        if (el) {
            el.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
    },
    expandActiveNavParents: function () {
        var active = document.querySelector('.navbar-vertical .nav-link.active');
        if (!active) return;

        var current = active.parentElement;
        while (current) {
            if (current.classList && current.classList.contains('collapse') && !current.classList.contains('navbar-collapse')) {
                current.classList.add('show');
                var toggler = document.querySelector('[data-bs-target="#' + current.id + '"]');
                if (toggler) {
                    toggler.setAttribute('aria-expanded', 'true');
                    toggler.classList.remove('collapsed');
                }
            }
            current = current.parentElement;
        }
    }
};
