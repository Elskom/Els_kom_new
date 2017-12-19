import zipfile
import os


def main():
    with zipfile.ZipFile('etc/site.zip', 'w', zipfile.ZIP_DEFLATED) as sitezip:
        sitezip.write('Tools/komformat.pye', 'komformat.pye')
        os.remove('Tools/komformat.pye')
    print('site.zip Created.')


if __name__ == "__main__":
    # execute main.
    main()
