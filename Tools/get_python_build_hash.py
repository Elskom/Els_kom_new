import sys
import os
import subprocess


def get_git_hash():
    return sys._git[2]


def get_current_git_hash():
    proc = subprocess.Popen(
        'git rev-parse --short HEAD ',
        stdout=subprocess.PIPE, universal_newlines=True)
    proc.wait()
    return proc.stdout


def main():
    if get_git_hash() == get_current_git_hash():
        print("Python build is already on latest commit.")
        return 0
    else:
        print("Rebuilding Python...")
        proc = subprocess.Popen(os.path.join(sys.path[5], 'PCbuild', 'build.bat'), universal_newlines=True)
        proc.wait()


main()
