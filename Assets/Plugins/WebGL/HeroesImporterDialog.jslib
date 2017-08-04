var HeroesImporterDialog = {

	ShowDialog: function(textPointer) {
		var text;
		var overlay;
		var container;
		var textArea;
		var importButton;
		var cancelButton;

		text = Pointer_stringify(textPointer);

		overlay = document.createElement('div');
		overlay.style.position = 'fixed';
		overlay.style.width = '100%';
		overlay.style.height = '100%';
		overlay.style.background = 'rgba(0, 0, 0, 0.6)';

		container = document.createElement('div');
		container.style.position = 'fixed';
		container.style.top = '50%';
		container.style.left = '50%';
		container.style.width = '500px';
		container.style.marginLeft = '-250px';
		container.style.height = '150px';
		container.style.marginTop = '-75px';

		textArea = document.createElement('textarea');
		textArea.style.display = 'block';
		textArea.style.width = '100%';
		textArea.style.boxSizing = 'border-box';
		textArea.style.resize = 'none';
		textArea.style.height = '150px';
		textArea.style.marginBottom = '10px';
		textArea.value = text;

		importButton = document.createElement('button');
		importButton.innerHTML = 'Import';
		importButton.style.float = 'right';
		importButton.style.marginLeft = '10px';
		importButton.addEventListener('click', function() {
			document.body.removeChild(overlay);
			SendMessage('Import/Export', 'UpdateHeroPanelsFromCsv', textArea.value);
		});

		cancelButton = document.createElement('button');
		cancelButton.innerHTML = 'Cancel';
		cancelButton.style.float = 'right';
		cancelButton.addEventListener('click', function() {
			document.body.removeChild(overlay);
		});

		container.appendChild(textArea);
		container.appendChild(importButton);
		container.appendChild(cancelButton);
		overlay.appendChild(container);
		document.body.appendChild(overlay);
	}
}

mergeInto(LibraryManager.library, HeroesImporterDialog);