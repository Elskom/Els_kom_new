#include <Windows.h>
#include "LoadResources.hpp"

using namespace System::Runtime::InteropServices;

namespace LoadResources {
  System::Drawing::Image ^ GetImageResource(int resource, int type) {
    HRSRC image_resource = FindResource(
      NULL, MAKEINTRESOURCE(resource), MAKEINTRESOURCE(type));
    unsigned int image_size = SizeofResource(NULL, image_resource);
    HGLOBAL image_global = LoadResource(NULL, image_resource);
    if (image_global == nullptr) {
      return nullptr;
    }
    cli::array<BYTE> ^MemPtr = gcnew array<BYTE>(image_size + 2);
    char * p_imagedata = (char *)LockResource(image_global);
    Marshal::Copy((System::IntPtr)p_imagedata, MemPtr, 0, image_size);
    System::IO::MemoryStream ^stream = gcnew System::IO::MemoryStream(MemPtr);
    stream->Write(MemPtr, 0, image_size);
    stream->Position = 0;
    System::Drawing::Image ^ptr_image = System::Drawing::Image::FromStream(stream);
    return  ptr_image;
  }

  System::Drawing::Icon ^ GetIconResource(int resource) {
    HICON hIcon = (HICON)LoadImage(GetModuleHandle(nullptr), MAKEINTRESOURCE(resource), IMAGE_ICON, 16, 16, LR_SHARED);
    System::Drawing::Icon^ ico;
    if (hIcon) {
      ico = System::Drawing::Icon::FromHandle((System::IntPtr)hIcon);
    }
    return ico;
  }

  System::Drawing::Icon ^ Get48x48IconResource(int resource) {
    HICON hIcon = (HICON)LoadImage(GetModuleHandle(nullptr), MAKEINTRESOURCE(resource), IMAGE_ICON, 48, 48, LR_SHARED);
    System::Drawing::Icon^ ico;
    if (hIcon) {
      ico = System::Drawing::Icon::FromHandle((System::IntPtr)hIcon);
    }
    return ico;
  }
}
