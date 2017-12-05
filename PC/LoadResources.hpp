#pragma once

namespace LoadResources {
  System::Drawing::Image ^ GetImageResource(int resource, int type);
  System::Drawing::Icon ^ GetIconResource(int resource);
  System::Drawing::Icon ^ Get48x48IconResource(int resource);
}
